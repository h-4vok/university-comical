using Comical.Models.Common;
using Comical.Models.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class ErrorLogRepository : BaseRepository
    {
        public void New(ErrorLogRecord model, int? userId)
        {
            this.UnitOfWork.ExecuteDirect(
                "ErrorLog_new",
                ParametersBuilder.With("ExceptionMessage", model.Message)
                .And("ExceptionSource", model.Source)
                .And("ExceptionType", model.Type)
                .And("ExceptionStackTrace", model.StackTrace)
                .And("UserId", userId)
            );
        }
    }
}
