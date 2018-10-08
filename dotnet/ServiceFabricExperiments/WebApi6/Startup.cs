using System.Web.Http;
using Autofac;
using Owin;

namespace WebApi6
{
    public static class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public static void ConfigureApp(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            appBuilder.UseDependencyInjection(config, RegisterDependencies);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }

        private static void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterModule<BusinessLogicModule>();
        }
    }
}
