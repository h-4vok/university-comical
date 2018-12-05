using Comical.Models;
using Comical.Models.ViewModels;
using DBNostalgia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class RoleRepository : BaseRepository
    {
        public IEnumerable<Role> Get()
        {
            var output = this.UnitOfWork.GetDirect(
                "Role_get",
                this.FetchRole
                );

            return output;
        }

        internal Role FetchRole(IDataReader reader)
        {
            var model = new Role
            {
                Id = reader.GetInt32("Id"),
                Code = reader.GetString("Code"),
                Description = reader.GetString("Description"),
                Enabled = reader.GetBoolean("Enabled")
            };

            return model;
        }

        public Role GetById(int roleId)
        {
            var output = this.UnitOfWork.GetDirect(
                "Role_getById",
                this.FetchRole,
                ParametersBuilder.With("Id", roleId)
            ).FirstOrDefault();

            return output;
        }

        public void New(RoleWithPermissionsViewModel role)
        {
            var permissionTableName = "RolePermission";

            this.UnitOfWork.Run(() =>
            {
                var id = this.UnitOfWork.Scalar(
                        "Role_new",
                        ParametersBuilder.With("Code", role.Code)
                        .And("Description", role.Description)
                        .And("Enabled", role.Enabled)
                    ).AsInt();

                foreach(var permission in role.Permissions)
                {
                    var relationshipId = this.UnitOfWork.Scalar(
                        "Role_newPermission",
                        ParametersBuilder.With("roleId", id)
                        .And("permissionId", permission.Key)
                    ).AsInt();

                    this.SetHorizontalVerifier(relationshipId, permissionTableName);
                }

                this.SetHorizontalVerifier(id);
                this.SetVerticalVerifierClosure();
                this.SetVerticalVerifierClosure(permissionTableName);
            });
        }

        public void Delete(int id)
        {
            this.UnitOfWork.Run(() =>
            {
                this.UnitOfWork.NonQuery(
                    "Role_delete",
                    ParametersBuilder.With("Id", id));

                this.SetVerticalVerifierClosure();
            });
        }

        public void Update(RoleWithPermissionsViewModel role)
        {
            var permissionTableName = "RolePermission";

            this.UnitOfWork.Run(() =>
            {
                this.UnitOfWork.NonQuery(
                        "Role_update",
                        ParametersBuilder.With("Code", role.Code)
                        .And("Description", role.Description)
                        .And("Enabled", role.Enabled)
                        .And("Id", role.Id)
                    );

                this.UnitOfWork.NonQuery(
                    "Role_deletePermissions",
                    ParametersBuilder.With("roleId", role.Id)
                );

                foreach (var permission in role.Permissions)
                {
                    var relationshipId = this.UnitOfWork.Scalar(
                        "Role_newPermission",
                        ParametersBuilder.With("roleId", role.Id)
                        .And("permissionId", permission.Key)
                    ).AsInt();

                    this.SetHorizontalVerifier(relationshipId, permissionTableName);
                }

                this.SetHorizontalVerifier(role.Id);
                this.SetVerticalVerifierClosure();
                this.SetVerticalVerifierClosure(permissionTableName);
            });
        }
    }
}
