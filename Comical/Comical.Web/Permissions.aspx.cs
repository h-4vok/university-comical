using Comical.Models;
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
    public partial class Permissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.LoadData();
        }

        private void LoadData()
        {
            var items = SecurityModelsService.obj.GetPermissions();

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
            
            void addNewRow(Permission model)
            {
                var row = table.NewRow();
                row[0] = model.Id;
                row[1] = model.Code;
                table.Rows.Add(row);
            }

            items.ForEach(addNewRow);

            this.Grid.DataSource = table;
            this.Grid.DataBind();

            this.lblError.Visible = false;
        }
    }
}