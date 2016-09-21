using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.BuilderProperties;
using MoneyBox.DataAccess;
using MoneyBox.Domain;
using MoneyBox.Services;
using MoneyBox.Web.Controllers;
using MoneyBox.Web.Providers;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace MoneyBox.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           // Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());

            InitializeSimpleInjector();
        }

        private static void InitializeSimpleInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<ApplicationDbContext>(() => new ApplicationDbContext(), Lifestyle.Scoped);
            // container.Register<DbContext, ApplicationDbContext>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);
            container.Register<IBoxService, BoxService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);

            // UserStore<TUser> is defined in Microsoft.AspNet.Identity.EntityFramework.
            // Do note that UserStore<TUser> implements IUserStore<TUser, string>, so
            // this Entity Framework provider requires a string. If you need int, you
            // might have your own store and need to build your own IUserStore implemenation.
            //container.Register<IUserStore<ApplicationUser, string>>(
            //    () => new UserStore<ApplicationUser> (),
            //    Lifestyle.Scoped);

            // Full controller registration
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
   //         container.RegisterWebApiControllers(config);

            container.Options.AllowOverridingRegistrations = true;

            container.Register<AccountController>(() => new AccountController(), Lifestyle.Scoped);

            container.Options.AllowOverridingRegistrations = false;

            // Manually controller registration
            //var registeredControllerTypes = SimpleInjectorMvcExtensions.GetControllerTypesToRegister(container, Assembly.GetExecutingAssembly())
            //    .Where(type => type.Name == "BoxController");

            //foreach (var controllerType in registeredControllerTypes)
            //{
            //    container.Register(controllerType, controllerType, Lifestyle.Scoped);
            //}

            container.RegisterSingleton<IOwinContextProvider>(new CallContextOwinContextProvider());

            container.RegisterMvcIntegratedFilterProvider();
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

    }
}
