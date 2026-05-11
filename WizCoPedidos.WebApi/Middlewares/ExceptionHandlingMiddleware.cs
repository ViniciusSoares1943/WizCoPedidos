using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WizCoPedidos.WebApi.Exceptions;

namespace WizCoPedidos.WebApi.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Erro não tratado na requisição");
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, errors) = exception switch
        {
            ValidationException validationException =>
                (StatusCodes.Status400BadRequest,
                 "Erro de validação",
                 validationException.Errors.Select(e => e.ErrorMessage).ToArray()),
            NotFoundException =>
                (StatusCodes.Status404NotFound, "Recurso não encontrado", Array.Empty<string>()),
            BadRequestException =>
                (StatusCodes.Status400BadRequest, "Requisição inválida", Array.Empty<string>()),
            _ =>
                (StatusCodes.Status500InternalServerError, "Erro interno no servidor", Array.Empty<string>())
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message
        };

        if (errors.Length > 0)
        {
            problem.Extensions["errors"] = errors;
        }

        return context.Response.WriteAsJsonAsync(problem);
    }
}
