using Comical.Models;
using Comical.Models.Common;
using Comical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class BackupService
    {
        #region Singleton

        private BackupService() { }
        static BackupService() { }

        public static BackupService obj { get; } = new BackupService();

        #endregion

        public IEnumerable<Backup> GetBackups()
        {
            var repository = new BackupRepository();
            var output = repository.Get();

            return output;
        }

        public void DoBackup(string username)
        {
            var repository = new BackupRepository();

            var backupPath = ComicalConfiguration.BackupPath;
            var filename = String.Format("Comical_{0}.bkp", DateTime.Now.ToString("yyyyMMdd_hhmmss"));

            repository.DoBackup(filename, username);
        }

        public void DoRestore(int backupId)
        {
            var repository = new BackupRepository();

            repository.DoRestore(backupId);
        }
    }
}
