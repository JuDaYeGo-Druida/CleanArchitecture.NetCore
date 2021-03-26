using AutoMapper;
using ig.estimador.application.WeatherForecast.Commands;
using ig.estimador.application.WeatherForecast.Dtos;
using Entities = ig.estimador.domain.Entities;

namespace ig.estimador.application.WeatherForecast.Mappings
{
    public class WeatherForecastMappingProfile : Profile
    {
        public WeatherForecastMappingProfile()
        {
            CreateMap<Entities.WeatherForecast, WeatherForecastDto>();
            CreateMap<CreateWeatherForecastCommand, Entities.WeatherForecast>();
        }
    }
}
