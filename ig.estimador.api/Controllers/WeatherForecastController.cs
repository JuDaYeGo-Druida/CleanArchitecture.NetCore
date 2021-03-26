using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ig.estimador.application.Common.Exceptions;
using ig.estimador.application.WeatherForecast.Commands;
using ig.estimador.application.WeatherForecast.Dtos;
using ig.estimador.application.WeatherForecast.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ig.estimador.api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class WeatherForecastController : ApiController
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<WeatherForecastDto>>> Get()
        {            
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> Post(CreateWeatherForecastCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("Division/{id}")]
        public async Task<ActionResult<double>> Division(int id)
        {
            double result = 25000 / id;
            return result;
        }
    }
}
