using Comical.Models.Logging;
using Comical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class LoggingService
    {
        #region Multi-Thread Safe Singleton

        private LoggingService() { }
        static LoggingService() { }

        public static LoggingService Instance { get; } = new LoggingService();

        #endregion

        private InformationLogRepository InfoLogRepository { get; } = new InformationLogRepository();
        private ErrorLogRepository ErrorLogRepository { get; } = new ErrorLogRepository();

        public void Log(string message, int? userId)
        {
            var info = new InfoLogRecord(message);
            this.InfoLogRepository.New(info, userId);
        }

        public void Log(Exception ex, int? userId)
        {
            var error = new ErrorLogRecord(ex);
            this.ErrorLogRepository.New(error, userId);

            if (ex.InnerException != null) this.Log(ex.InnerException, userId);
        }
    }
}
