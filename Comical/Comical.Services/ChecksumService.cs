using Comical.Models;
using Comical.Models.Common;
using Comical.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Services
{
    public class ChecksumService
    {
        #region Singleton

        private ChecksumService() { }
        static ChecksumService() { }

        public static ChecksumService obj { get; } = new ChecksumService();

        #endregion
        
        
    }
}
