using AutoMapper;
using ig.estimador.application.Common.Interfaces;
using ig.estimador.application.UnitTests.Common.Interfaces;
using ig.estimador.application.WeatherForecast.Commands;
using ig.estimador.application.WeatherForecast.Mappings;
using ig.estimador.application.WeatherForecast.Queries;
using NUnit.Framework;
using System;
using System.Threading;

namespace ig.estimador.application.UnitTests.WeatherForecast
{
    [TestFixture]
    public class WeatherForecastHandlers
    {
        private IApplicationDbContext AplicacionContexto;
        private IMapper Mapper;

        [SetUp]
        public void SetUp()
        {
            var weatherForecastMappingProfile = new WeatherForecastMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(weatherForecastMappingProfile));

            Mapper = new Mapper(configuration);
            AplicacionContexto = new MockApplicationDbContext().ApplicationDbContext();
        }

        [Test]
        public void GetWeatherForecasts()
        {
            var handle = new GetWeatherForecastsHandle(AplicacionContexto, Mapper);
            var query = new GetWeatherForecastsQuery();

            var result = handle.Handle(query, new CancellationToken());

            Assert.IsNotNull(result.Result);
        }

        [Test]
        public void AddWeatherForecast()
        {
            var handle = new CreateWeatherForecastHandler(AplicacionContexto, Mapper);
            var command = new CreateWeatherForecastCommand 
            {
                Date = DateTime.Now,
                TemperatureC = 34,
                Summary = "Fecha de prueba"
            };

            var result = handle.Handle(command, new CancellationToken());

            Assert.AreEqual(6, result.Result);
        }
    }
}
