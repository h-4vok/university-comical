using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models
{
    public class DatabaseStatus
    {
        public string DatabaseName { get; set; }
        public bool UnderMaintenance { get; set; }
        public bool HasChecksumError { get; set; }
    }
}
