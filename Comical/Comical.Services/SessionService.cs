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
    }
}
