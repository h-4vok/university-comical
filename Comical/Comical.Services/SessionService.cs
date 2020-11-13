using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class SessionService : ISessionService
    {
        public int CurrentUserId { get; set; }
        public string CurrentUserName { get; set; }
        public bool IsAuthenticated { get { return this.CurrentUserId > 0; } }
        public IEnumerable<string> Permissions { get; set; } = new List<string>();
    }
}
