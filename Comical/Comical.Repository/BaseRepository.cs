using Comical.Models.Common;
using DBNostalgia;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public abstract class BaseRepository
    {
        protected class RecordModel
        {
            public IEnumerable<string> Values { get; set; }
            public string Verifier { get; set; }
            public int Id { get; set; }
            public object Timestamp { get; set; }
        }

        public BaseRepository(string tableName = null, string connectionString = null)
        {
            connectionString = connectionString ?? ComicalConfiguration.ComicalConnectionString;
            this.UnitOfWork = new UnitOfWork(() => this.BuildConnection(connectionString));
            this.TableName = tableName ?? this.CalculatedTableName;
        }

        protected IDbConnection BuildConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            return connection;
        }

        protected string GetCalculatedTableName(string className) => className.Replace("Repository", String.Empty);

        protected string CalculatedTableName => this.GetCalculatedTableName(this.GetType().Name);

        protected string TableName { get; }

        protected UnitOfWork UnitOfWork { get; set; }

        private readonly IEnumerable<string> columnsToAvoid = new List<string> { "__Timestamp__", "__HorizontalVerifier__" };

        protected string CreateWhere(int recordId)
        {
            const string format = "Id = '{0}'";
            var where = String.Format(format, recordId);

            return where;
        }

        protected string CalculateHorizontalVerifier(int recordId)
        {
            var where = this.CreateWhere(recordId);
            var checksum = this.CalculateHorizontalVerifier(where);

            return checksum;
        }

        protected string CalculateHorizontalVerifier(string where, string table = null)
        {
            table = table ?? this.TableName;
            var values = this.GetRecordValues(where, table);
            var output = this.CalculateHorizontalVerifier(values);

            return output;
        }

        protected string CalculateHorizontalVerifier(IEnumerable<string> values)
        {
            var output = Crypto3DES.obj.GetChecksum(values);

            return output;
        }

        protected void SetHorizontalVerifier(int id, string table)
        {
            var where = this.CreateWhere(id);
            var checksum = this.CalculateHorizontalVerifier(where, table);
            this.SetHorizontalVerifier(checksum, where, table);
        }

        protected void SetHorizontalVerifier(int id)
        {
            this.SetHorizontalVerifier(id, this.TableName);
        }

        protected IEnumerable<string> GetRecordValues(string where, string table = null)
        {
            table = table ?? this.TableName;
            var values = this.UnitOfWork.Get(
                "Security_getRecord",
                this.FetchRecordValues,
                ParametersBuilder.With("table", table)
                .And("where", where)
            ).First();

            return values;
        }

        protected IEnumerable<string> FetchRecordValues(IDataReader reader)
        {
            var output = new List<string>();
            var schemaTable = reader.GetSchemaTable();
            var columns = schemaTable
                .Select()
                .Select(r => r.ItemArray[0].AsString())
                .ToList();

            columns.RemoveAll(columnsToAvoid.Contains);

            var values = columns
                .Select(c => reader.GetValue(c).AsString())
                .ToList();

            return values;
        }

        protected void SetHorizontalVerifier(string verifier, string where, string table)
        {
            table = table ?? this.TableName;
            this.UnitOfWork.NonQuery(
                "Security_setVerifier",
                ParametersBuilder.With("table", table)
                .And("verifier", verifier)
                .And("where", where)
            );
        }

        protected void SetHorizontalVerifierDirectly(int id, string verifier)
        {
            var where = this.CreateWhere(id);
            this.UnitOfWork.NonQueryDirect(
                "Security_setVerifier",
                ParametersBuilder.With("table", this.TableName)
                .And("verifier", verifier)
                .And("where", where)
            );
        }

        public void SetVerticalVerifierClosure(string table = null)
        {
            table = table ?? this.TableName;

            var verifiers = this.UnitOfWork.Get(
                    "Security_getHorizontalVerifiers",
                    this.FetchHorizontalVerifier,
                    ParametersBuilder.With("TableName", table)
                );

            var verticalChecksum = Crypto3DES.obj.GetChecksum(verifiers);

            this.UnitOfWork.NonQuery(
                "VerticalVerifier_update",
                ParametersBuilder.With("TableName", table)
                .And("VerticalVerifier", verticalChecksum)
            );
        }

        public void SetVerticalVerifier(string table = null)
        {
            table = table ?? this.TableName;

            this.UnitOfWork.Run(() =>
            {
                SetVerticalVerifierClosure(table);  
            });
        }

        protected string FetchHorizontalVerifier(IDataReader reader)
        {
            var output = reader.GetString("__HorizontalVerifier__");
            return output;
        }

        protected RecordModel FetchRecordModel(IDataReader reader)
        {
            var output = new RecordModel();
            var values = new List<string>();
            var schemaTable = reader.GetSchemaTable();
            var columns = schemaTable
                .Select()
                .Select(r => r.ItemArray[0].AsString())
                .ToList();

            output.Timestamp = new object(); // TODO: complete this
            output.Verifier = reader.GetString("__HorizontalVerifier__");
            output.Id = reader.GetInt32("Id");

            columns.RemoveAll(columnsToAvoid.Contains);

            values.AddRange(columns.Select(c => reader.GetValue(c).AsString()));
            output.Values = values;

            return output;
        }

        public void ResetHorizontalVerifiers()
        {
            var models = this.UnitOfWork.GetDirect(
                "Security_getAllRecords",
                this.FetchRecordModel,
                ParametersBuilder.With("table", this.TableName)
            );

            Parallel.ForEach(models,
                new ParallelOptions { MaxDegreeOfParallelism = ComicalConfiguration.ChecksumResetDOP },
                model =>
                {
                    var verifier = this.CalculateHorizontalVerifier(model.Values);
                    this.SetHorizontalVerifierDirectly(model.Id, verifier);
                }
            );
        }

        public IEnumerable<string> FindChecksumErrors()
        {
            var output = new ConcurrentBag<string>();

            var models = this.UnitOfWork.GetDirect(
                "Security_getAllRecords",
                this.FetchRecordModel,
                ParametersBuilder.With("table", this.TableName)
            );

            const string errorFormat = "Dígito Verificador Horizontal Inválido. Tabla '{0}'. Id '{1}'.";

            Parallel.ForEach(models,
                new ParallelOptions { MaxDegreeOfParallelism = ComicalConfiguration.ChecksumCheckByModelDOP },
                model =>
                {
                    var lazyVerify = new Lazy<string>(() => this.CalculateHorizontalVerifier(model.Values));
                    var verifierIsInvalid = String.IsNullOrWhiteSpace(model.Verifier) || lazyVerify.Value != model.Verifier;

                    if (verifierIsInvalid)
                    {
                        var message = String.Format(errorFormat, this.TableName, model.Id);
                        output.Add(message);
                    }
                });

            return output.ToList();
        }
    }
}
