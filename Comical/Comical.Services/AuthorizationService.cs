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
        public bool IsEnabledFor(int userId, string permissionCode)
        {
            var repository = new PermissionRepository();
            var isGranted = repository.IsGrantedTo(userId, permissionCode);

            return isGranted;
        }
    }
}
