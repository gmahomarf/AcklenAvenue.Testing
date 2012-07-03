using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace AcklenAvenue.Data.Sample.MVC
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //need to bootstrap the application
            var container = new Container();
            new WebSampleBootstrapper(container).Run();

            AreaRegistration.RegisterAllAreas();
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            
        }
    }
}