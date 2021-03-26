using Dapper;
using ig.estimador.application.Common.Interfaces;
using ig.estimador.domain.Entities;
using ig.estimador.infrastructure.Common;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ig.estimador.infrastructure.Persistence
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string STOREPROCEDURE_GET = "sp_WeatherForecast_Get";
        private readonly string STOREPROCEDURE_ADD = "sp_WeatherForecast_Add";

        public WeatherForecastRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(WeatherForecast entity)
        {
            using (var connection = ConnectionFactory.Connection(_configuration))
            {
                connection.Open();

                var values = new
                {
                    Date = entity.Date,
                    TemperatureC = entity.TemperatureC,
                    TemperatureF = entity.TemperatureF,
                    Summary = entity.Summary,
                    CreatedBy = "Jyepes", // Tomar datos del token de identidad
                };

                var result = await connection.QuerySingleAsync<int>(STOREPROCEDURE_ADD, values, commandType: CommandType.StoredProcedure);
                connection.Close();

                return result;
            }
        }

        public Task<int> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherForecast> Get(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            using (var connection = ConnectionFactory.Connection(_configuration))
            {
                connection.Open();

                var result = await connection.QueryAsync<domain.Entities.WeatherForecast>(STOREPROCEDURE_GET, commandType: CommandType.StoredProcedure);
                connection.Close();

                return result;
            }
        }

        public Task<int> Update(WeatherForecast entity)
        {
            throw new NotImplementedException();
        }
    }
}
