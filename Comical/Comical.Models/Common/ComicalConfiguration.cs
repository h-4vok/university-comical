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

        public static string ComicalAPIUri => ConfigurationManager.AppSettings["ComicalAPI.Uri"];
        public static string BackupPath => ConfigurationManager.AppSettings["Backup.Path"];

        public static int ChecksumCheckDOP => ConfigurationManager.AppSettings["ChecksumCheck.DOP"].AsInt();
        public static int ChecksumCheckByModelDOP => ConfigurationManager.AppSettings["ChecksumCheckByModel.DOP"].AsInt();
        public static int ChecksumResetDOP => ConfigurationManager.AppSettings["ChecksumReset.DOP"].AsInt();
    }
}
