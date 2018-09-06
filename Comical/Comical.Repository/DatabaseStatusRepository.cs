using Comical.Models;
using Comical.Models.Common;
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
        public DatabaseStatusRepository() : base(null, new UnitOfWork(ComicalConfiguration.MasterConnectionString))
        {
        }

        public void SetUnderMaintenace(bool underMaintenance)
        {
            this.UnitOfWork.ExecuteDirect(
                "DatabaseStatus_setMaintenance",
                ParametersBuilder.With("DatabaseName", "Comical")
                .And("UnderMaintenance", underMaintenance)
            );
        }

        public void SetHasChecksumError(bool hasChecksumError)
        {
            this.UnitOfWork.ExecuteDirect(
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
