using Comical.Models;
using Comical.Models.Enums;
using Comical.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comical.Web
{
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.LoadData();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this.Master is SiteMaster master)
            {
                var canEdit = master.UserHasPermission(PermissionCodes.Roles_CanEdit);
                this.Grid.Columns[4].Visible = canEdit;
                this.Grid.Columns[5].Visible = canEdit;
                this.NewRoleButton.Visible = canEdit;
            }
        }

        private void LoadData()
        {
            var items = SecurityModelsService.obj.GetRoles();

            if (!items.Any())
            {
                this.lblError.Text = "No se encontraron registros.";
                this.lblError.Visible = true;
                this.Grid.Visible = false;
                return;
            }

            var table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Code");
            table.Columns.Add("Description");
            table.Columns.Add("Enabled", typeof(bool));

            void addNewRow(Role model)
            {
                var row = table.NewRow();
                row[0] = model.Id;
                row[1] = model.Code;
                row[2] = model.Description;
                row[3] = model.Enabled;
                table.Rows.Add(row);
            }

            items.ForEach(addNewRow);

            this.Grid.DataSource = table;
            this.Grid.DataBind();

            this.lblError.Visible = false;
        }

        protected void Grid_Delete(object source, DataGridCommandEventArgs e)
        {

        }

        protected void Grid_Update(object source, DataGridCommandEventArgs e)
        {

        }

        protected void NewRoleButton_Click(object source, EventArgs e)
        {
            Response.Redirect("Role.aspx");
        }
    }
}