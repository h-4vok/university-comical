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
        internal Role FetchRole(IDataReader reader)
        {
            var model = new Role
            {
                Code = reader.GetString("Code"),
                Description = reader.GetString("Description")
            };

            return model;
        }
    }
}
