using Autofac.Integration.Web;
using EndPoint.WebForm.App_Start;
using System;
using System.Web;

namespace EndPoint.WebForm
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        private IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider { get { return _containerProvider; } }

        protected void Application_Start(object sender, EventArgs e)
        {
            var container = AutofacConfig.BuildContainer();

            _containerProvider = new ContainerProvider(container);
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