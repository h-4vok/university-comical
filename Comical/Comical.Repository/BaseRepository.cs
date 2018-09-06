using Comical.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public abstract class BaseRepository
    {
        public BaseRepository(string tableName = null)
        {
            this.UnitOfWork = new UnitOfWork();
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
    }
}
