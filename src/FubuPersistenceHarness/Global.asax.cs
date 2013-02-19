using System;
using System.Web;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using StructureMap;

namespace FubuPersistenceHarness
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            FubuApplication.For<HarnessRegistry>()
                .StructureMap(new Container())
                .Bootstrap();
        }
    }
}