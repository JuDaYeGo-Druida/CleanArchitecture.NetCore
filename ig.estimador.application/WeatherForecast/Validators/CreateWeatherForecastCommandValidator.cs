using FluentValidation;
using ig.estimador.application.WeatherForecast.Commands;

namespace ig.estimador.application.WeatherForecast.Validators
{
    public class CreateWeatherForecastCommandValidator : AbstractValidator<CreateWeatherForecastCommand>
    {
        public CreateWeatherForecastCommandValidator()
        {
            RuleFor(t => t.Date).NotEmpty().WithMessage("La fecha es requerida");
            RuleFor(t => t.TemperatureC).Must(t => t >= -273 && t <= 800).NotEmpty().WithMessage("Temperatura no válida");
            RuleFor(t => t.Summary).MaximumLength(100).NotEmpty();
        }
    }
}
