using Comical.Models.Enums;
using Comical.Models.Http;
using Comical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Comical.API.Controllers
{
    public class BackupController : ApiController
    {
        public DoBackupResponse Post(DoBackupRequest request)
        {
            return null;
        }

        public DoRestoreResponse Put(int backupId, DoRestoreRequest request)
        {
            return null;
        }
    }
}
