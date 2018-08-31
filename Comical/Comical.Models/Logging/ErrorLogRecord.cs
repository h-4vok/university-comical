using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.Logging
{
    public class ErrorLogRecord : LogRecord
    {
        public ErrorLogRecord(Exception ex)
        {
            this.Message = ex.Message;
            this.Type = ex.GetType().FullName;
            this.Source = ex.Source;
            this.StackTrace = ex.StackTrace;
        }

        public string Type { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
    }
}
