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
    public class ChecksumService
    {
        #region Singleton

        private ChecksumService() { }
        static ChecksumService() { }

        public static ChecksumService obj { get; } = new ChecksumService();

        protected IEnumerable<BaseRepository> repositories = new List<BaseRepository>
        {
            new HistoryEventRepository(),
            new HistoryExceptionRepository(),
            new PermissionRepository(),
            new RoleRepository(),
            new UserRepository(),
        };

        #endregion

        public void SetDatabaseToChecksumError()
        {
            var repository = new DatabaseStatusRepository();
            repository.SetHasChecksumError(true);
        }

        public IEnumerable<string> CheckVerifiers()
        {
            var errors = new List<string>();

            Parallel.ForEach(this.repositories,
                new ParallelOptions { MaxDegreeOfParallelism = ComicalConfiguration.ChecksumCheckDOP },
                r =>
                {
                    var messages = r.FindChecksumErrors();
                    errors.AddRange(messages);
                });

            return errors;
        }

        public void ResetVerticalVerifiers()
        {
            Parallel.ForEach(this.repositories, r =>
            {
                r.SetVerticalVerifier();
            });
        }
    }
}
