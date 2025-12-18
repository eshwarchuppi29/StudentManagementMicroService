using System.Net;
using System.Text.Json;
using FluentValidation;
using StudentManagementSystem.GlobalValidation.GlobalErrorResponse;

namespace StaffMicroService.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "Unhadled exception occurred");
                await HandlerExceptionAsync(context, ex);
            }
        }

        private Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "An unexpected error occurred",
                Detail = exception.Message,
                TraceId = context.TraceIdentifier,
                LineNumber = exception.StackTrace?.Split(Environment.NewLine).FirstOrDefault()
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }
}
