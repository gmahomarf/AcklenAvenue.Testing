using System.Web.Mvc;
using System.Web.Routing;
using AcklenAvenue.Data.NHibernate;
using AcklenAvenue.Data.Sample.DataLayer;
using AcklenAvenue.Data.Sample.Domain;
using AcklenAvenue.Data.Sample.MVC.Models;
using AutoMapper;
using StructureMap;
using IMappingEngine = AutoMapper.IMappingEngine;

namespace AcklenAvenue.Data.Sample.MVC
{
    public class WebSampleBootstrapper : BootstrapperBase
    {
        readonly Container _container;

        public WebSampleBootstrapper(Container container) : base(container)
        {
            _container = container;
        }

        public override void Run()
        {
            base.Run();

            _container.Configure(x =>
                {
                    x.For<IAccountFetcher>().Use<AccountFetcher>();
                    x.For<IRepository>().Use<Repository>();
                    x.For<IMappingEngine>().Use(Mapper.Engine);
                    //x.For<IControllerFactory>().Use(() => null);
                    //x.For<IControllerActivator>().Use(() => null);
                });

            //register this action filter to open and close the NHibernate session at the beginning and end of each request
            GlobalFilters.Filters.Add(new SessionManagementFilter(_container.GetInstance<ISessionContainer>()));

            //register a dependency resolver so that controllers and their dependencies can be resolved
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(_container));
            
            ConfigureAutomappings();

            RegisterRoutes();            
        }

        void ConfigureAutomappings()
        {
            Mapper.CreateMap<Account, AccountModel>();
        }

        public static void RegisterRoutes()
        {
            RouteCollection routes = RouteTable.Routes;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }
    }
}