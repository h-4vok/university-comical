using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.Common
{
    public static class ComicalConfiguration
    {
        public static string ComicalConnectionString => ConfigurationManager.ConnectionStrings["ComicalDB"].ConnectionString;
        public static string MasterConnectionString => ConfigurationManager.ConnectionStrings["MasterDB"].ConnectionString;
    }
}
