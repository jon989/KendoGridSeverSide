using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Razor;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace KndoGrid1
{
    public class AppHost : AppHostBase
    {
        //Tell ServiceStack the name of your application and where to find your services
        public AppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

        public override void Configure(Funq.Container container)
        {
            
            //register any dependencies your services use, e.g:
            //container.Register<ICacheClient>(new MemoryCacheClient());

            JsConfig.EmitCamelCaseNames = true;
            SetConfig(new HostConfig
            {
                AppendUtf8CharsetOnContentTypes = new HashSet<string> { MimeTypes.Html },
                AllowJsonpRequests = true,
                AllowFileExtensions = { "json", "cshtml" }
            }
          );
            var conString = ConfigurationManager.ConnectionStrings["Portal"].ConnectionString;
            var conFactory = new OrmLiteConnectionFactory(conString, SqlServerDialect.Provider, true);
            container.Register<IDbConnectionFactory>(c => conFactory);
            Plugins.Add(new RazorFormat());
           
            Plugins.Add(new AutoQueryFeature() {
                EnableRawSqlFilters = true,
                EnableUntypedQueries = true
            });
        }

    }
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            new AppHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}