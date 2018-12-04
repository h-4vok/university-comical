using Comical.Models.Enums;
using Comical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comical.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected ISessionService sessionService = DependencyResolver.obj.Resolve<ISessionService>();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.SetupSessionService();

            this.SetupMenuVisibility();
        }

        internal void SetupMenuVisibility()
        {
            if (this.sessionService.IsAuthenticated)
            {
                this.menuOptionLogin.Visible = false;
                this.menuOptionSignUp.Visible = false;

                this.menuOptionHistoryEvents.Visible = this.sessionService.Permissions.Contains(PermissionCodes.InfoLogging_CanRead);
                this.menuOptionHistoryExceptions.Visible = this.sessionService.Permissions.Contains(PermissionCodes.ErrorLogging_CanRead);
                this.menuOptionVerifiers.Visible = this.sessionService.Permissions.Contains(PermissionCodes.VerifierDigits_CanRead);
                this.menuOptionBackups.Visible = this.sessionService.Permissions.Contains(PermissionCodes.BackupAndRestore);

                this.menuOptionPermissions.Visible = this.sessionService.Permissions.Contains(PermissionCodes.Permission_CanRead);
            }
            else
            {
                this.menuOptionHistoryEvents.Visible = false;
                this.menuOptionHistoryExceptions.Visible = false;
                this.menuOptionVerifiers.Visible = false;
                this.menuOptionBackups.Visible = false;

                this.menuOptionPermissions.Visible = false;
            }
        }

        internal void SetupSessionService()
        {
            sessionService.CurrentUserId = Session["UserId"].AsInt();
            sessionService.CurrentUserName = Session["UserName"].AsString();
            if (sessionService.CurrentUserId > 0)
                sessionService.Permissions = AuthorizationService.obj.GetPermissionCodes(sessionService.CurrentUserId);
        }

        internal string GetCurrentUserName()
        {
            return sessionService.CurrentUserName;
        }
    }
}