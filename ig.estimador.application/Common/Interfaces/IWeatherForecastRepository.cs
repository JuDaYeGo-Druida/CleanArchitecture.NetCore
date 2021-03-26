using Entities = ig.estimador.domain.Entities;

namespace ig.estimador.application.Common.Interfaces
{
    public interface IWeatherForecastRepository : IGenericRepository<Entities.WeatherForecast>
    {
    }
}
