using System.Diagnostics;
using System.Text.Json;
using CQRS_mediatR.Domain.Validators.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_mediatR.Infrastructure.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public GlobalExceptionHandlingMiddleware(
        ILogger<GlobalExceptionHandlingMiddleware> logger,
        IHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante a execução da requisição: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = context.Request.Path,
            Extensions = { ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier }
        };

        switch (exception)
        {

            case DatabaseException ex:

                problemDetails.Title = "Database Error";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = ex.Message;
                break;
            case Exception ex:
                problemDetails.Title = "An error occurred while attempt processing you request";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = ex.Message;
                break;

            default:
                _logger.LogError(exception, "Erro não tratado: {Message}", exception.Message);
                problemDetails.Title = "Erro Interno do Servidor";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = "Ocorreu um erro interno no servidor";
                break;
        }

        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = "application/problem+json";

        await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails,
        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}