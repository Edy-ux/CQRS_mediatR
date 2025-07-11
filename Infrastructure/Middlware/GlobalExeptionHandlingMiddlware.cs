using System.Diagnostics;
using System.Text.Json;
using CQRS_mediatR.Domain.Validators.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_mediatR.Infrastructure.Middleware;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    /// <summary>
    /// A machine-readable format for specifying errors in HTTP API responses based on <see href="https://tools.ietf.org/html/rfc7807"/>.
    /// </summary>
    private const string RFC_PROBLEM_TYPE = "https://tools.ietf.org/html/rfc7807";

    private const string CONTENT_TYPE = "application/problem+json";


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
            _logger.LogError(ex, "Error during the execution of the request: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = CreateBaseProblemDetails(context);

        ConfigureExceptionDetails(problemDetails, exception);

        await SendErrorResponse(context, problemDetails);
    }

    private ProblemDetails CreateBaseProblemDetails(HttpContext context)
    {
        return new ProblemDetails
        {
            Type = RFC_PROBLEM_TYPE,
            Instance = context.Request.Path,
            Extensions = { ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier }
        };
    }

    private void ConfigureExceptionDetails(ProblemDetails problemDetails, Exception exception)
    {
        problemDetails.Status = StatusCodes.Status500InternalServerError;
        
        switch (exception)
        {
            case DatabaseException dbEx:
                ConfigureProblemDetails(problemDetails, "Database Error", dbEx.Message);
                break;

            case Exception ex:
                ConfigureProblemDetails(problemDetails,
                    "An error occurred while processing your request",
                    ex.Message);
                break;

            default:
                _logger.LogError(exception, "Unhandled error: {Message}", exception.Message);
                ConfigureProblemDetails(problemDetails,
                    "Internal Server Error",
                    "An internal error occurred on the server");
                break;
        }
    }

    private void ConfigureProblemDetails(ProblemDetails details, string title, string detail)
    {
        details.Title = title;
        details.Detail = detail;
    }

    private async Task SendErrorResponse(HttpContext context, ProblemDetails problemDetails)
    {
        context.Response.StatusCode = problemDetails.Status.Value;
        context.Response.ContentType = CONTENT_TYPE;

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails, jsonOptions);
    }
}