using Comical.Models.Common;
using System;
using System.Collections.Generic;
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

    }
}
