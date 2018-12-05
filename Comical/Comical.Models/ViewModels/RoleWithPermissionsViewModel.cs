using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.ViewModels
{
    public class RoleWithPermissionsViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public IDictionary<int, Permission> Permissions { get; } = new Dictionary<int, Permission>();
    }
}
