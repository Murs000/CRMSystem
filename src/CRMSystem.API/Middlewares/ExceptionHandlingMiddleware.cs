using System.Net;
using System.Text.Json;
using CRMSystem.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CRMSystem.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while processing the request."); // Log the exception
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;
        var problemDetails = new ProblemDetails();

        switch (ex)
        {
            case ApplicationException:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Detail = ex.Message;
                problemDetails.Title = "Application Error";
                _logger.LogWarning("Application error: {Message}", ex.Message); // Log application-specific warning
                break;

            case KeyNotFoundException:
            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Detail = ex.Message;
                problemDetails.Title = "Not Found";
                _logger.LogWarning("Resource not found: {Message}", ex.Message); // Log not-found error as a warning
                break;

            case ValidationException exc:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails = new ValidationProblemDetails(exc.Errors)
                {
                    Detail = ex.Message,
                    Title = "Validation Error"
                };
                problemDetails.Extensions.Add("invalidParams", exc.Errors);
                _logger.LogWarning("Validation error: {Errors}", exc.Errors); // Log validation errors
                break;

            case UnAuthorizedException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                problemDetails.Detail = ex.Message;
                problemDetails.Title = "Unauthorized";
                _logger.LogWarning("Unauthorized access attempt: {Message}", ex.Message); // Log unauthorized access as a warning
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                problemDetails.Detail = ex.Message;
                problemDetails.Title = "Server Error";
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message); // Log unhandled exceptions
                break;
        }

        var result = JsonSerializer.Serialize(problemDetails);
        await context.Response.WriteAsync(result);
    }
}