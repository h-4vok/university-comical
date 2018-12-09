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

        /// <summary>
        /// Configuramos la visibilidad del menú según las opciones de seguridad.
        /// </summary>
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
                this.menuOptionRoles.Visible = this.sessionService.Permissions.Contains(PermissionCodes.Roles_CanRead);
            }
            else
            {
                this.menuOptionHistoryEvents.Visible = false;
                this.menuOptionHistoryExceptions.Visible = false;
                this.menuOptionVerifiers.Visible = false;
                this.menuOptionBackups.Visible = false;

                this.menuOptionPermissions.Visible = false;
                this.menuOptionRoles.Visible = false;
            }
        }

        /// <summary>
        /// Indica si el usuario actual posee o no un permiso específico.
        /// </summary>
        /// <param name="permissionCode"></param>
        /// <returns></returns>
        internal bool UserHasPermission(string permissionCode)
        {
            if (!sessionService.Permissions.Any())
                sessionService.Permissions = AuthorizationService.obj.GetPermissionCodes(sessionService.CurrentUserId);

            var output = this.sessionService.Permissions.Contains(permissionCode);
            return output;
        }

        /// <summary>
        /// Configuramos el servicio SessionService para conocer en todo momento qué usuario está autenticado.
        /// </summary>
        internal void SetupSessionService()
        {
            sessionService.CurrentUserId = Session["UserId"].AsInt();
            sessionService.CurrentUserName = Session["UserName"].AsString();
            if (sessionService.CurrentUserId > 0)
                sessionService.Permissions = AuthorizationService.obj.GetPermissionCodes(sessionService.CurrentUserId);
        }

        /// <summary>
        /// Método que permite a las páginas contenido de esta master conocer al usuario actual.
        /// </summary>
        /// <returns></returns>
        internal string GetCurrentUserName()
        {
            return sessionService.CurrentUserName;
        }
    }
}