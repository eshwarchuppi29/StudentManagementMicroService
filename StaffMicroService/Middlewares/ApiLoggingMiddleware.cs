using StaffMicroService.Repositories.Implementation;
using StaffMicroService.Repositories.Interfaces;
using StudentMangementSystem.Model.Models;
using StudentMangementSystem.Model.Response;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace StaffMicroService.Middlewares
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiLoggingMiddleware> _logger;

        public ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IServiceScopeFactory scopeFactory)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            string requestBody = await ReadRequestBodyAsync(context);
            string responseBody = string.Empty;

            // Swap the response body stream for reading later
            var originalBodyStream = context.Response.Body;
            using var newResponseBody = new MemoryStream();
            context.Response.Body = newResponseBody;

            try
            {
                // Continue pipeline
                await _next(context);

                // Read the response now
                responseBody = await ReadResponseBodyAsync(context);
                await WriteResponseToClient(context, originalBodyStream, responseBody);
            }
            catch (Exception ex)
            {
                var lineNumber = GetExceptionLineNumber(ex);
                var errorResponse = ResponseFactory.Error(new
                {
                    Message = ex.Message,
                    Exception = ex.GetType().Name,
                    LineNumber = lineNumber
                });

                responseBody = JsonSerializer.Serialize(errorResponse);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await WriteResponseToClient(context, originalBodyStream, responseBody);
            }
            finally
            {
                stopwatch.Stop();

                // Log to DB using Scoped service
                using var scope = scopeFactory.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IApiLogRepository>();
                await repo.SaveAsync(new ApiLog
                {
                    TraceId = context.TraceIdentifier,
                    RequestPath = context.Request.Path,
                    RequestMethod = context.Request.Method,
                    RequestBody = requestBody,
                    ResponseBody = responseBody,
                    StatusCode = context.Response.StatusCode,
                    ExecutionTimeMs = stopwatch.ElapsedMilliseconds
                });
            }
        }

        // -------------------------------
        // Read Response Body
        // -------------------------------
        private async Task<string> ReadResponseBodyAsync(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string body = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }

        // -------------------------------
        // Read Request Body
        // -------------------------------
        private async Task<string> ReadRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                leaveOpen: true
            );

            string body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            return body;
        }

        // -------------------------------
        // Write Response Back to Client
        // -------------------------------
        private async Task WriteResponseToClient(HttpContext context, Stream originalBody, string response)
        {
            var bytes = Encoding.UTF8.GetBytes(response);
            await originalBody.WriteAsync(bytes, 0, bytes.Length);
            context.Response.Body = originalBody;
        }

        // -------------------------------
        // Extract Line Number
        // -------------------------------
        private int GetExceptionLineNumber(Exception ex)
        {
            var stack = new StackTrace(ex, true);
            var frame = stack.GetFrame(stack.FrameCount - 1);
            return frame?.GetFileLineNumber() ?? 0;
        }
    }
}