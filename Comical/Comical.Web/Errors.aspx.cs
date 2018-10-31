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
    public partial class Errors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.LoadData();
        }

        private void LoadData()
        {
            var items = LoggingService.obj.GetExceptions();

            if (!items.Any())
            {
                this.lblError.Text = "No se encontraron registros.";
                this.lblError.Visible = true;
                this.Grid.Visible = false;
                return;
            }

            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Tipo");
            table.Columns.Add("Fuente");
            table.Columns.Add("Mensaje");
            table.Columns.Add("Usuario", typeof(int));
            table.Columns.Add("Fecha", typeof(DateTime));
            
            void addNewRow(HistoryException model)
            {
                var row = table.NewRow();
                row[0] = model.Id;
                row[1] = model.ExceptionType;
                row[2] = model.ExceptionSource;
                row[3] = model.ExceptionMessage;
                row[4] = model.UserId;
                row[5] = model.DateLogged;
                table.Rows.Add(row);
            }

            items.ForEach(addNewRow);

            this.Grid.DataSource = table;
            this.Grid.DataBind();

            this.lblError.Visible = false;
        }
    }
}