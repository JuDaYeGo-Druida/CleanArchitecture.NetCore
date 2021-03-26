using System;
using System.Collections.Generic;
using System.Text;

namespace ig.estimador.application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        IWeatherForecastRepository WeatherForecastRepository { get; set; }
    }
}
