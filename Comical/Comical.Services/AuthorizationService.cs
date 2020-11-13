using Comical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class AuthorizationService
    {
        #region Singleton

        private AuthorizationService() { }
        static AuthorizationService() { }

        public static AuthorizationService obj { get; } = new AuthorizationService();

        #endregion

        public bool IsEnabledFor(int userId, string permissionCode)
        {
            var repository = new PermissionRepository();
            var isGranted = repository.IsGrantedTo(userId, permissionCode);

            return isGranted;
        }

        public IEnumerable<string> GetPermissionCodes(int userId)
        {
            var repository = new PermissionRepository();
            var codes = repository.GetBy(userId);

            return codes;
        }
    }
}
