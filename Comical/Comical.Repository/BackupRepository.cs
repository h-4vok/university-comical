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
    public class BackupRepository : BaseRepository
    {
        public BackupRepository() : base(null, ComicalConfiguration.MasterConnectionString)
        {
        }

        public void DoBackup(string filepath, string createdBy)
        {
            this.UnitOfWork.NonQueryDirect("Database_doBackup_Comical_full",
                ParametersBuilder.With("filepath", filepath)
                );

            this.UnitOfWork.NonQueryDirect("Backup_new",
                ParametersBuilder.With("filepath", filepath)
                .And("CreatedBy", createdBy));
        }

        public void DoRestore(int backupId)
        {
            var model = this.UnitOfWork.GetOneDirect("Backup_getOne",
                this.Fetch,
                ParametersBuilder.With("id", backupId));

            this.UnitOfWork.NonQueryDirect("Security_doRestore",
                ParametersBuilder.With("filepath", model.FilePath)
                .And("dbname", "Comical")
                );
        }

        public IEnumerable<Backup> Get()
        {
            var items = this.UnitOfWork.GetDirect("Backup_get",
                this.Fetch
                );

            return items;
        }

        protected Backup Fetch(IDataReader reader)
        {
            var item = new Backup
            {
                Id = reader.GetInt32("Id"),
                FilePath = reader.GetString("FilePath"),
                BackupDate = reader.GetDateTime("BackupDate"),
                CreatedBy = reader.GetString("CreatedBy")
            };

            return item;
        }
    }
}
