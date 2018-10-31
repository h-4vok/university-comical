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
    public partial class Events : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.LoadData();
        }

        private void LoadData()
        {
            var items = LoggingService.obj.GetEvents();

            if (!items.Any())
            {
                this.lblError.Text = "No se encontraron registros.";
                this.lblError.Visible = true;
                this.Grid.Visible = false;
                return;
            }

            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Sección");
            table.Columns.Add("Mensaje");
            table.Columns.Add("Usuario", typeof(int));
            table.Columns.Add("Fecha", typeof(DateTime));
            
            void addNewRow(HistoryEvent model)
            {
                var row = table.NewRow();
                row[0] = model.Id;
                row[1] = model.Section;
                row[2] = model.Message;
                row[3] = model.UserId;
                row[4] = model.DateLogged;
                table.Rows.Add(row);
            }

            items.ForEach(addNewRow);

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