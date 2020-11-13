using Comical.Models;
using Comical.Models.Common;
using DBNostalgia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class PermissionRepository : BaseRepository
    {
        public bool IsGrantedTo(int userId, string permissionCode)
        {
            var output = this.UnitOfWork.ScalarDirect(
                "Permission_isGrantedToUser",
                ParametersBuilder.With("userId", userId)
                .And("permissionCode", permissionCode)
            ).AsBool();

            return output;
        }

        public IEnumerable<string> GetBy(int userId)
        {
            var output = this.UnitOfWork.GetDirect(
                "Permission_getByUser",
                this.FetchPermissionCodes,
                ParametersBuilder.With("userId", userId)
            );

            return output;
        }

        public IEnumerable<Permission> Get()
        {
            var output = this.UnitOfWork.GetDirect(
                "Permission_get",
                this.Fetch
                );

            return output;
        }

        protected string FetchPermissionCodes(IDataReader reader)
        {
            var code = reader.GetString("Code");
            return code;
        }

        public IEnumerable<Permission> GetByRole(int roleId)
        {
            var output = this.UnitOfWork.GetDirect(
                "Permission_getByRole",
                this.Fetch,
                ParametersBuilder.With("roleId", roleId)
            );

            return output;
        }

        protected Permission Fetch(IDataReader reader)
        {
            var item = new Permission
            {
                Id = reader.GetInt32("Id"),
                Code = reader.GetString("Code")
            };

            return item;
        }

    }
}
