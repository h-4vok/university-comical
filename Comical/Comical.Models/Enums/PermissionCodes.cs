using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comical.Models.Enums
{
    public static class PermissionCodes
    {
        public static readonly string VerifierDigits_CheckOnLogin = "VerifierDigits_CheckOnLogin";
        public static readonly string User_New = "User_New";
        public static readonly string User_Update = "User_Update";
        public static readonly string User_Disable = "User_Disable";
        public static readonly string User_Enable = "User_Enable";
        public static readonly string User_Delete = "User_Delete";
        public static readonly string User_Read = "User_Read";
        public static readonly string ShoppingCart_CanUse = "ShoppingCart_CanUse";
        public static readonly string Account_CanUse = "Account_CanUse";
        public static readonly string InfoLogging_CanRead = "InfoLogging_CanRead";
        public static readonly string ErrorLogging_CanRead = "ErrorLogging_CanRead";
        public static readonly string UnderMaintenance_CanLogin = "UnderMaintenance_CanLogin";
        public static readonly string HasChecksumError_CanLogin = "HasChecksumError_CanLogin";
        public static readonly string VerifierDigits_CanFix = "VerifierDigits_CanFix";
        public static readonly string VerifierDigits_CanRead = "VerifierDigits_CanRead";
        public static readonly string BackupAndRestore = "BackupAndRestore";
        public static readonly string Permission_CanRead = "Permission_CanRead";
        public static readonly string Roles_CanRead = "Roles_CanRead";
        public static readonly string Roles_CanEdit = "Roles_CanEdit";
    }
}
