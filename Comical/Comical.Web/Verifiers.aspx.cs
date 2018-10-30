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
    public partial class Verifiers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.LoadData();
        }

        private void LoadData()
        {
            var errors = ChecksumService.obj.CheckVerifiers();

            if (!errors.Any())
            {
                this.lblError.Text = "No se encontraron errores.";
                this.lblError.Visible = true;
                this.Grid.Visible = false;
                this.RecalculateButton.Visible = false;
                return;
            }

            var table = new DataTable();
            table.Columns.Add("#", typeof(int));
            table.Columns.Add("Error");

            var count = 1;

            void addNewRow(string error)
            {
                var row = table.NewRow();
                row["#"] = count;
                row["Error"] = error;
                table.Rows.Add(row);

                count++;
            }

            errors.ForEach(addNewRow);

            this.Grid.DataSource = table;
            this.Grid.DataBind();

            this.lblError.Visible = false;
        }

        protected void RecalculateButton_Click(object sender, EventArgs e)
        {
            var task = ComicalAPIService.obj.RecalculateVerifiers();
            Task.WaitAll(new[] { task });

            if (task.Result)
            {
                this.LoadData();
            }
            else
            {
                this.lblError.Text = "No se pudieron corregir los dígitos verificadores.";
                this.lblError.Visible = true;
            }
        }
    }
}