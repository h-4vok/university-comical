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
    public partial class Backups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                this.LoadData();
        }

        private void LoadData()
        {
            var items = BackupService.obj.GetBackups();

            if (!items.Any())
            {
                this.ShowError("No se encontraron registros.");
                return;
            }

            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Path");
            table.Columns.Add("Fecha", typeof(DateTime));
            table.Columns.Add("Usuario");
            
            
            void addNewRow(Backup model)
            {
                var row = table.NewRow();
                row[0] = model.Id;
                row[1] = model.FilePath;
                row[2] = model.BackupDate;
                row[3] = model.CreatedBy;
                table.Rows.Add(row);
            }

            items.ForEach(addNewRow);

            this.Grid.DataSource = table;
            this.Grid.DataBind();

            this.lblError.Visible = false;
        }

        protected void DoBackup_Click(object sender, EventArgs e)
        {
            if (this.Master is SiteMaster master)
            {
                var username = master.GetCurrentUserName();

                try
                {
                    DatabaseStatusService.obj.SetUnderMaintenance();
                    BackupService.obj.DoBackup(username);
                }
                finally
                {
                    DatabaseStatusService.obj.UnsetUnderMaintenance();
                    this.LoadData();
                }
            }
            else
            {
                this.ShowError("El usuario no se encuentra autenticado.");
                return;
            }
        }

        protected void ShowError(string errorMessage)
        {
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
            this.Grid.Visible = false;
        }

        protected void DoRestore_Click(object sender, EventArgs e)
        {
            var restoreIDText = this.restoreID.Value;
            
            if(Int32.TryParse(restoreIDText, out var restoreID))
            {
                try
                {
                    DatabaseStatusService.obj.SetUnderMaintenance();
                    BackupService.obj.DoRestore(restoreID);
                }
                finally
                {
                    DatabaseStatusService.obj.UnsetUnderMaintenance();
                }
            }
            else
            {
                this.ShowError("El ID ingresado no es válido.");
            }
        }
    }
}