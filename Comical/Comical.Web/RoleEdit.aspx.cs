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

            if (!this.IsPostBack && this.CurrentRoleId > 0)
            {
                this.LoadModel();
            }
        }

        protected void LoadModel()
        {
            var model = SecurityModelsService.obj.GetRole(this.CurrentRoleId);

            this.codeInput.Value = model.Code;
            this.descriptionInput.Value = model.Description;
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

            var role = new Comical.Models.Role
            {
                Id = this.CurrentRoleId,
                Code = this.codeInput.Value,
                Description = this.descriptionInput.Value,
                Enabled = true,
            };

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