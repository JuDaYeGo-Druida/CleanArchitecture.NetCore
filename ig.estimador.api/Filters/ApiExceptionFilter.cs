using ig.estimador.application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ig.estimador.api.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        public ApiExceptionFilter()
        {
            // Registra tipos y manejadores de excepciones conocidas
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            // Si excepción conocida, se registrará un manejador
            TryHandleException(context);
            // Si no, se usa comportamiento por defecto.
            base.OnException(context);
        }

        private void TryHandleException(ExceptionContext context)
        {
            Log.Error(context.Exception, "Manejando Excepción:");
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStatusException(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var excepcion = context.Exception as NotFoundException;
            var details = new ProblemDetails()
            {
                Status = 404,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "El recurso específicado no fué encontrado.",
                Detail = excepcion.Message
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var excepcion = context.Exception as ValidationException;
            var details = new ValidationProblemDetails(excepcion.Errors)
            {
                Status = 404,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = excepcion.StackTrace
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStatusException(ExceptionContext context)
        {
            ValidationProblemDetails details = new ValidationProblemDetails(context.ModelState)
            {
                Status = 422,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = context.Exception.StackTrace
            };

            context.Result = new BadRequestObjectResult(details);
            context.ExceptionHandled = true;
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            ProblemDetails details = new ProblemDetails
            {
                Status = 500,
                Title = "Error inesperado.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Detail = context.Exception.Message
            };

            context.Result = new ObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
