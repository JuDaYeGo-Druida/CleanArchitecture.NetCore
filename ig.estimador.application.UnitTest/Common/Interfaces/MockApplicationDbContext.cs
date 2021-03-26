using System.Collections.Generic;
using System.Threading.Tasks;
using ig.estimador.application.Common.Interfaces;
using Entities = ig.estimador.domain.Entities;
using Moq;
using System.IO;
using Newtonsoft.Json;

namespace ig.estimador.application.UnitTests.Common.Interfaces
{
    public class MockApplicationDbContext
    {
        public IApplicationDbContext ApplicationDbContext()
        {
            Mock<IApplicationDbContext> mockContexto = new Mock<IApplicationDbContext>();
            mockContexto.Setup(setup => setup.WeatherForecastRepository).Returns(WeatherForecastRepositoryMock());
            return mockContexto.Object;
        }

        private IWeatherForecastRepository WeatherForecastRepositoryMock()
        {
            Mock<IWeatherForecastRepository> mockWeather = new Mock<IWeatherForecastRepository>();

            mockWeather.Setup(setup => setup.GetAll()).Returns(getWeathers());

            mockWeather.Setup(setup => setup.Add(It.IsAny<Entities.WeatherForecast>()))
               .Returns(addWeather());

            return mockWeather.Object;
        }

        private Task<int> addWeather()
        {
            return Task.FromResult<int>(6);
        }

        private Task<IEnumerable<Entities.WeatherForecast>> getWeathers()
        {
            List<Entities.WeatherForecast> items;

            using (StreamReader r = new StreamReader("DataMock\\WeatherForecastData.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Entities.WeatherForecast>>(json);
            }

            return Task.FromResult<IEnumerable<Entities.WeatherForecast>>(items);
        }
    }
}
