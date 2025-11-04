using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EIMS.API.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Try to let the request continue through the pipeline
                await _next(context);
            }
            catch (ValidationException ex)
            {
                // This is the specific exception thrown by our ValidationBehaviour
                _logger.LogWarning(ex, "Validation error occurred: {Message}", ex.Message);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/problem+json";

                // Group errors by property name (e.g., "PrefixID", "InvoiceTypeID")
                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                    .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

                // Create a standard ValidationProblemDetails object
                var problemDetails = new ValidationProblemDetails(errors)
                {
                    Title = "One or more validation errors occurred.",
                    Status = StatusCodes.Status400BadRequest,
                };

                // Write the clean JSON response
                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
            catch (Exception ex)
            {
                // Catch-all for any other unexpected errors
                _logger.LogError(ex, "An unhandled exception has occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/problem+json";

                var problemDetails = new ProblemDetails
                {
                    Title = "An internal server error occurred.",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message // Only show detail in development
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
            }
        }
    }
}
