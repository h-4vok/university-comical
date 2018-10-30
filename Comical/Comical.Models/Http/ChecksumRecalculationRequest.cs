using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.Http
{
    public class ChecksumRecalculationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
