using Comical.Models.Common;
using Comical.Models.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Repository
{
    public class InformationLogRepository : BaseRepository
    {
        public void New(InfoLogRecord model, int? userId)
        {
            this.UnitOfWork.ExecuteDirect(
                "InformationLog_new",
                ParametersBuilder.With("Message", model.Message)
                .And("UserId", userId)
            );
        }
    }
}
