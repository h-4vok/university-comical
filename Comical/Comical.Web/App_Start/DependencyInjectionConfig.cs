using Comical.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comical.Web
{
    public static class DependencyInjectionConfig
    {
        public static void Config()
        {
            DependencyResolver.obj.Register<ISessionService, SessionService>();
        }
    }
}