using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.ServiceFabric;
using Microsoft.ServiceFabric.Services.Runtime;

namespace WebApi6
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                /*ServiceRuntime.RegisterServiceAsync("WebApi6Type",
                    context => new WebApi6(context)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(WebApi6).Name);

                // Prevents this host process from terminating so services keeps running. 
                Thread.Sleep(Timeout.Infinite);*/

                var builder = new ContainerBuilder();

                //builder.RegisterModule(new BusinessLogicModule());
                builder.RegisterServiceFabricSupport();
                builder.RegisterStatelessService<WebApi6>("WebApi6Type");

                //builder.RegisterType<IBusinessLogic>();

                using (builder.Build())
                {
                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
