using System;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;

namespace WebApi6
{
    public static class AppBuilderExtensions
    {
        public static void UseDependencyInjection(this IAppBuilder app, HttpConfiguration config, Action<ContainerBuilder> registerAction)
        {
            var builder = new ContainerBuilder();
            registerAction(builder);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
        }
    }
}
