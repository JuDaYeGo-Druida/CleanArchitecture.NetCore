using ig.estimador.application.Common.Interfaces;
using ig.estimador.infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ig.estimador.infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection servicios)
        {

            servicios.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            servicios.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
            return servicios;
        }
    }
}
