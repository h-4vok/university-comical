{
﻿using Comical.Models;
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

        #region Thread-safe Singleton

        private LoggingService() { }
        static LoggingService() { }

        private static readonly LoggingService instance = new LoggingService();

        public static LoggingService obj => instance;

        #endregion

        private Lazy<ISessionService> lazySessionService = new Lazy<ISessionService>(() => DependencyResolver.obj.Resolve<ISessionService>());

        private ISessionService SessionService => lazySessionService.Value;

        public void Log(string section, string message)
        {
            var repository = new HistoryEventRepository();
            var userId = this.SessionService.CurrentUserId;

            var item = new HistoryEvent
            {
                Section = section,
                Message = message,
                UserId = userId
            };

            repository.New(item);
        }

        public void Log(string section, Exception ex)
        {
            var repository = new HistoryExceptionRepository();
            var userId = this.SessionService.CurrentUserId;

            var item = new HistoryException
            {
                Section = section,
                ExceptionType = ex.GetType().FullName,
                ExceptionSource = ex.Source,
                ExceptionMessage = ex.Message,
                ExceptionStackTrace = ex.StackTrace,
                UserId = userId
            };

            repository.New(item);

            if (ex.InnerException != null) this.Log(section, ex.InnerException);
        }

    }
}
