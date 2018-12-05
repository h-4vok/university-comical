using Comical.Models;
using Comical.Models.ViewModels;
using Comical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comical.Web
{
    public partial class RoleEdit : System.Web.UI.Page
    {
        protected int CurrentRoleId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CurrentRoleId = Request.QueryString["Id"].AsInt();

            if (!this.IsPostBack)
            {
                this.LoadPermissionsList();

                if (this.CurrentRoleId > 0)
                {
                    this.LoadModel();
                }
            }
        }

        protected void LoadPermissionsList()
        {
            var permissions = SecurityModelsService.obj.GetPermissions();

            permissions
                .Select(p => new ListItem(p.Code, p.Id.AsString()))
                .ForEach(this.permissionsList.Items.Add);
        }

        protected void LoadModel()
        {
            var model = SecurityModelsService.obj.GetRoleWithPermissions(this.CurrentRoleId);

            this.codeInput.Value = model.Code;
            this.descriptionInput.Value = model.Description;

            foreach (ListItem item in this.permissionsList.Items)
            {
                var permissionId = item.Value.AsInt();
                item.Selected = model.Permissions.ContainsKey(permissionId);
            }
        }

        protected void ShowError(string errorMessage)
        {
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
        }

        protected void ActionButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.codeInput.Value))
            {
                this.ShowError("El código no puede ser vacío.");
                return;
            }

            if (String.IsNullOrEmpty(this.descriptionInput.Value))
            {
                this.ShowError("La descripción no puede ser vacío.");
                return;
            }

            var role = new RoleWithPermissionsViewModel
            {
                Id = this.CurrentRoleId,
                Code = this.codeInput.Value,
                Description = this.descriptionInput.Value,
                Enabled = true,
            };

            foreach(ListItem item in this.permissionsList.Items)
            {
                if (item.Selected)
                {
                    var permissionId = item.Value.AsInt();

                    role.Permissions.Add(permissionId, new Permission { Id = permissionId, Code = item.Text });
                }
            }

            if (role.Id > 0)
            {
                SecurityModelsService.obj.UpdateRole(role);
            }
            else
            {
                SecurityModelsService.obj.CreateRole(role);
            }

            Response.Redirect("Roles");
        }
    }
}