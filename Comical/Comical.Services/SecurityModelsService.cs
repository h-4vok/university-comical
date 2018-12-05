using Comical.Models;
using Comical.Models.ViewModels;
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

        public RoleWithPermissionsViewModel GetRoleWithPermissions(int roleId)
        {
            var output = new RoleWithPermissionsViewModel();
            var role = this.Roles.GetById(roleId);
            output.Id = role.Id;
            output.Code = role.Code;
            output.Description = role.Description;
            output.Enabled = role.Enabled;

            var permissions = this.Permissions.GetByRole(roleId);
            permissions
                .ForEach(p => output.Permissions.Add(p.Id, p));

            return output;
        }

        public IEnumerable<Role> GetRoles()
        {
            var output = this.Roles.Get();

            return output;
        }

        public void CreateRole(RoleWithPermissionsViewModel role)
        {
            this.Roles.New(role);
        }

        public void UpdateRole(RoleWithPermissionsViewModel role)
        {
            this.Roles.Update(role);
        }

        public void DeleteRole(int id)
        {
            this.Roles.Delete(id);
        }
    }
}
