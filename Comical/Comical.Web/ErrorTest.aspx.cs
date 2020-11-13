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
    public partial class ErrorTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            throw new SystemException("Este es un error de prueba global.");
        }
    }
}