using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models
{
    public class User : BaseModel
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public bool Blocked { get; set; }
        public int Retries { get; set; }

        public IEnumerable<Role> Roles { get; set; }
    }
}
