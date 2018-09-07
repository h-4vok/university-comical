using Comical.Models.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
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

        public BaseRepository(string tableName = null, UnitOfWork unitOfWork = null)
        {
            this.UnitOfWork = unitOfWork ?? new UnitOfWork();
            this.TableName = tableName ?? this.CalculatedTableName;
        }

        protected string CalculatedTableName => this.GetType().Name.Replace("Repository", String.Empty);

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

        protected string CalculateHorizontalVerifier(string where)
        {
            var values = this.GetRecordValues(where);
            var output = this.CalculateHorizontalVerifier(values);

            return output;
        }

        protected string CalculateHorizontalVerifier(IEnumerable<string> values)
        {
            var output = Crypto3DES.obj.GetChecksum(values);

            return output;
        }

        protected void SetHorizontalVerifier(int id)
        {
            var where = this.CreateWhere(id);
            var checksum = this.CalculateHorizontalVerifier(where);
            this.SetHorizontalVerifier(checksum, where);
        }

        protected IEnumerable<string> GetRecordValues(string where)
        {
            var values = this.UnitOfWork.GetDirect(
                "Security_getRecord",
                this.FetchRecordValues,
                ParametersBuilder.With("table", this.TableName)
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

            var values = columns.Select(c => reader.GetValue(c).AsString());

            return values;
        }

        protected void SetHorizontalVerifier(string verifier, string where)
        {
            this.UnitOfWork.ExecuteDirect(
                "Security_setVerifier",
                ParametersBuilder.With("table", this.TableName)
                .And("verifier", verifier)
                .And("where", where)
            );
        }

        public void SetVerticalVerifier()
        {
            var verifiers = this.UnitOfWork.GetDirect(
                "Security_getHorizontalVerifiers",
                this.FetchHorizontalVerifier,
                ParametersBuilder.With("table", this.TableName)
            );

            var verticalChecksum = Crypto3DES.obj.GetChecksum(verifiers);

            this.UnitOfWork.ExecuteDirect(
                "VerticalVerifier_update",
                ParametersBuilder.With("TableName", this.TableName)
                .And("VerticalVerifier", verticalChecksum)
            );
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

        public IEnumerable<string> FindChecksumErrors()
        {
            var output = new ConcurrentBag<string>();
            
            var models = this.UnitOfWork.GetDirect(
                "Security_getAllRecords",
                this.FetchRecordModel,
                ParametersBuilder.With("table", this.TableName)
            );

            const string errorFormat = "Dígito Verificador Horizontal Inválido. Tabla '{0}'. Id '{1}'.";

            Parallel.ForEach(models, model =>
            {
                var verifier = this.CalculateHorizontalVerifier(model.Values);

                if (verifier != model.Verifier)
                {
                    var message = String.Format(errorFormat, this.TableName, model.Id);
                    output.Add(message);
                }
            });

            return output.ToList();
        }
    }
}
