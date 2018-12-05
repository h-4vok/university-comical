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
    }
}
