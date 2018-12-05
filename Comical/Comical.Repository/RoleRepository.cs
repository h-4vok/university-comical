using Comical.Models;
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

        public void New(Role role)
        {
            this.UnitOfWork.Run(() =>
            {
                var id = this.UnitOfWork.Scalar(
                        "Role_new",
                        ParametersBuilder.With("Code", role.Code)
                        .And("Description", role.Description)
                        .And("Enabled", role.Enabled)
                    ).AsInt();

                this.SetHorizontalVerifier(id);
                this.SetVerticalVerifierClosure();
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

        public void Update(Role role)
        {
            this.UnitOfWork.Run(() =>
            {
                this.UnitOfWork.NonQuery(
                        "Role_update",
                        ParametersBuilder.With("Code", role.Code)
                        .And("Description", role.Description)
                        .And("Enabled", role.Enabled)
                        .And("Id", role.Id)
                    );

                this.SetHorizontalVerifier(role.Id);
                this.SetVerticalVerifierClosure();
            });
        }
    }
}
