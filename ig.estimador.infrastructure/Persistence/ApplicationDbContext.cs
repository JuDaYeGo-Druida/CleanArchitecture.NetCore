using ig.estimador.application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ig.estimador.infrastructure.Persistence
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        public ApplicationDbContext(IWeatherForecastRepository weatherForecastRepository)
        {
            WeatherForecastRepository = weatherForecastRepository;
        }

        public IWeatherForecastRepository WeatherForecastRepository { get; set; }
    }
}
