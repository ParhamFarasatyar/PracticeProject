using System;
using System.Web;
using Autofac.Integration.Web;
using EndPoint.WebForm.App_Start;

namespace EndPoint.WebForm;

public class Global : HttpApplication, IContainerProviderAccessor
{
    private static IContainerProvider _containerProvider;

    public IContainerProvider ContainerProvider => _containerProvider;

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