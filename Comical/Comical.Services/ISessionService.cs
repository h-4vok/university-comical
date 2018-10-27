using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public interface ISessionService
    {
        int CurrentUserId { get; set; }
        bool IsAuthenticated { get; }
        IEnumerable<string> Permissions { get; set; }
    }
}
