using Comical.Models;
using Comical.Models.Common;
using DBNostalgia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class DatabaseStatusRepository : BaseRepository
    {
        public DatabaseStatusRepository() : base(null, ComicalConfiguration.MasterConnectionString)
        {
        }

        public void KillAllConnections()
        {
            this.UnitOfWork.NonQueryDirect(
                "Database_killAll",
                ParametersBuilder.With("DBName", "Comical")
            );
        }

        public void SetUnderMaintenace(bool underMaintenance)
        {
            this.UnitOfWork.NonQueryDirect(
                "DatabaseStatus_setMaintenance",
                ParametersBuilder.With("DatabaseName", "Comical")
                .And("UnderMaintenance", underMaintenance)
            );
        }

        public void SetHasChecksumError(bool hasChecksumError)
        {
            this.UnitOfWork.NonQueryDirect(
                "DatabaseStatus_setHasChecksumError",
                ParametersBuilder.With("DatabaseName", "Comical")
                .And("HasChecksumError", hasChecksumError)
            );
        }

        public DatabaseStatus Get()
        {
            var model = this.UnitOfWork.GetDirect(
                "DatabaseStatus_get",
                this.Fetch,
                ParametersBuilder.With("DatabaseName", "Comical")
            ).First();

            return model;
        }

        protected DatabaseStatus Fetch(IDataReader reader)
        {
            var model = new DatabaseStatus
            {
                DatabaseName = reader.GetString("DatabaseName"),
                UnderMaintenance = reader.GetBoolean("UnderMaintenance"),
                HasChecksumError = reader.GetBoolean("HasChecksumError")
            };

            return model;
        }
    }
}
