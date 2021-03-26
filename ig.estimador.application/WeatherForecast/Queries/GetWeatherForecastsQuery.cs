using AutoMapper;
using ig.estimador.application.Common.Interfaces;
using ig.estimador.application.WeatherForecast.Dtos;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ig.estimador.application.WeatherForecast.Queries
{
    public class GetWeatherForecastsQuery : IRequest<List<WeatherForecastDto>>
    {
    }
    public class GetWeatherForecastsHandle : IRequestHandler<GetWeatherForecastsQuery, List<WeatherForecastDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWeatherForecastsHandle(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<WeatherForecastDto>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.WeatherForecastRepository.GetAll();
            return _mapper.Map<List<WeatherForecastDto>>(result.ToList());
        }
    }

}
