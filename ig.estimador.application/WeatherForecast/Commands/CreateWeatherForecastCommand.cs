using AutoMapper;
using ig.estimador.application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Entities = ig.estimador.domain.Entities;

namespace ig.estimador.application.WeatherForecast.Commands
{
    public class CreateWeatherForecastCommand : IRequest<int>
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }

    public class CreateWeatherForecastHandler : IRequestHandler<CreateWeatherForecastCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateWeatherForecastHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var weatherForecast = _mapper.Map<Entities.WeatherForecast>(request);
            weatherForecast.TemperatureF = 32 + (int)(weatherForecast.TemperatureC / 0.5556);

            var result = await _context.WeatherForecastRepository.Add(weatherForecast);
            return result;
        }
    }

}
