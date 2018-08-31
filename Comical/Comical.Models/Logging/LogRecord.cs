using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.Logging
{
    public abstract class LogRecord
    {
        public LogRecord()
        {
            this.DateTime = DateTime.Now;
        }

        public DateTime DateTime { get; set; }
        public string Message { get; set; }
    }
}
