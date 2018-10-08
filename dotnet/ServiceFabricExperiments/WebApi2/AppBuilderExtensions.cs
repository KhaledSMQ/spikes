using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using SpikesCo.Platform.Web.Swashbuckle;
using Owin;
using Swashbuckle.Application;

namespace WebApi2
{
    public static class AppBuilderExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "app")]
        public static void UseSwagger(this IAppBuilder app, HttpConfiguration config, string version, string title)
        {
            config.EnableSwagger(c =>
                {
                    c.SingleApiVersion(version, title);
                    c.IncludeAllXmlComments();
                    c.UseFullTypeNameInSchemaIds();
                })
                .EnableSwaggerUi();
        }
    }
}
