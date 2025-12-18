using StudentManagementSystem.GlobalValidation.GlobalErrorResponse;
using FluentValidation;
using System.Net;

namespace StaffMicroService.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ValidationMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new ValidationErrorResponse
                {
                    Errors = ex.Errors.Select(e => new ValidationError
                    {
                        Field = e.PropertyName,
                        Message = e.ErrorMessage,
                        Code = e.ErrorCode
                    }).ToList()
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
