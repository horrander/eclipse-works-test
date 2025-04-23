using System.Net;
using EclipseWorks.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EclipseWorks.WebApi;

public class ExceptionFilters : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        var logger = LogError();

        if (context.Exception is BusinessException businessException)
        {
            context.Result = CreateResult(businessException, new
            {
                businessException.BusinessError.Key,
                businessException.BusinessError.Message
            });

            logger.LogError(context.Exception, "Erro ao processar solicitação: \nKey: {}\nMessage: {}",
                businessException.BusinessError.Key,
                businessException.BusinessError.Message);
        }
        else if (context.Exception is Exception exception)
        {
            context.Result = CreateResult(exception, new
            {
                Message = "Não foi possível concluir a solicitação, consulte os logs para mais detalhes."
            });

            logger.LogError(exception, "Erro ao processar solicitação: \nMessage: {}",
                exception.InnerException?.Message ?? exception.Message);
        }
        //TODO: Filtrar novos tipos de exceptions

        context.ExceptionHandled = true;
    }

    private static ObjectResult CreateResult(Exception exception, object value, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        var result = new ObjectResult(exception)
        {
            StatusCode = (int)statusCode,
            Value = value
        };

        return result;
    }

    private static ILogger LogError()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        return factory.CreateLogger("EclipseWorks.ExceptionFilter");
        //
    }
}
