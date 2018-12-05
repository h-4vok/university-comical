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

        private PermissionRepository Permissions { get; } = new PermissionRepository();
        private RoleRepository Roles { get; } = new RoleRepository();

        public IEnumerable<Permission> GetPermissions()
        {
            var output = this.Permissions.Get();

            return output;
        }

        public Role GetRole(int roleId)
        {
            var output = this.Roles.GetById(roleId);

            return output;
        }

        public IEnumerable<Role> GetRoles()
        {
            var output = this.Roles.Get();

            return output;
        }

        public void CreateRole(Role role)
        {
            this.Roles.New(role);
        }

        public void UpdateRole(Role role)
        {
            this.Roles.Update(role);
        }

        public void DeleteRole(int id)
        {
            this.Roles.Delete(id);
        }
    }
}
