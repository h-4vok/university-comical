using Comical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Comical.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            var login = this.loginInput.Value;
            var password = this.passwordInput.Value;

            var service = new AuthenticationService();
            var authenticateResponse = service.Authenticate(login, password);

            if (authenticateResponse.ChecksumErrors.Any())
            {
                const string checksumErrorsFormat = "Atención. Se encontraron {0} errores en dígitos verificadores.";
                this.lblError.Text = String.Format(checksumErrorsFormat, authenticateResponse.ChecksumErrors.Count());
                this.lblError.Visible = true;
            }
            else if (authenticateResponse.Authenticated)
            {
                this.lblError.Visible = false;
                Session.Add("UserId", authenticateResponse.UserId);
                Response.Redirect("Default.aspx");
            }
            else
            {
                this.lblError.Text = authenticateResponse.ValidationError;
                this.lblError.Visible = true;
            }
        }
    }
}