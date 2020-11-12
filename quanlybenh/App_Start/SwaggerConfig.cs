using System.Web.Http;
using WebActivatorEx;
using quanlybenh;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace quanlybenh
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                      
                        c.SingleApiVersion("v1", "quanlybenh");

                      
                    })
                .EnableSwaggerUi(c =>
                    {
                       
                    });
        }
    }
}
