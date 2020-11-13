﻿using Comical.Services;
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

        private void SetSuccessLabels()
        {
            this.divUser.Visible = false;
            this.divPassword.Visible = false;
            this.LoginButton.Visible = false;

            this.divSuccess.Visible = true;
            this.lblSuccess.Visible = true;
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            var login = this.loginInput.Value;
            var password = this.passwordInput.Value;

            var service = new AuthenticationService();
            var authenticateResponse = service.Authenticate(login, password);

            if (authenticateResponse.ChecksumErrors.Any())
            {
                Session.Add("UserId", authenticateResponse.UserId);
                const string checksumErrorsFormat = "Atención. Se encontraron {0} errores en dígitos verificadores.";
                this.lblError.Text = String.Format(checksumErrorsFormat, authenticateResponse.ChecksumErrors.Count());
                this.lblError.Visible = true;

                this.SetSuccessLabels();

                if (this.Master is SiteMaster master)
                {
                    master.SetupSessionService();
                    master.SetupMenuVisibility();
                }
            }
            else if (authenticateResponse.Authenticated)
            {
                this.lblError.Visible = false;
                Session.Add("UserId", authenticateResponse.UserId);
                Session.Add("UserName", login);
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