using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.IO;

namespace ig.estimador.api.Config
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            var basepath = System.AppDomain.CurrentDomain.BaseDirectory;
            var xmlpath = Path.Combine(basepath, "ig.estimador.api.xml");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo { Title = "Estimador API V1", Version = "V1" });
                c.IncludeXmlComments(xmlpath);
            }
            );

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddRegistration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "Estimador API V1");
            });

            return app;
        }
    }
}
