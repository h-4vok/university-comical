using Comical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class DatabaseStatusService
    {
        #region Singleton

        private DatabaseStatusService() { }
        static DatabaseStatusService() { }

        public static DatabaseStatusService obj { get; } = new DatabaseStatusService();

        #endregion

        private DatabaseStatusRepository repository { get; } = new DatabaseStatusRepository();

        public void KillAllConnections() => this.repository.KillAllConnections();

        public void SetUnderMaintenance() => this.repository.SetUnderMaintenace(true);

        public void UnsetUnderMaintenance() => this.repository.SetUnderMaintenace(false);

        public void SetHasChecksumError() => this.repository.SetHasChecksumError(true);

        public void UnsetHasChecksumError() => this.repository.SetHasChecksumError(false);
    }
}
