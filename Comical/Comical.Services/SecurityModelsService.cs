using Comical.Models;
using Comical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class SecurityModelsService
    {
        #region Singleton

        private SecurityModelsService() { }
        static SecurityModelsService() { }

        public static SecurityModelsService obj { get; } = new SecurityModelsService();

        #endregion

        public IEnumerable<Permission> GetPermissions()
        {
            var repository = new PermissionRepository();
            var output = repository.Get();

            return output;
        }
    }
}
