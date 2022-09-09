using FluentValidation;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Common.Response;

namespace TodoMinimalApi.Middleware
{
    /// <summary>
    /// Middleware handling custom exceptions
    /// </summary>
    public class ErrorHandlingMiddleware : IMiddleware
    {
 
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ApplicationExceptionBase e)
            {
                context.Response.StatusCode = e.HttpStatusCode;
                await context.Response.WriteAsJsonAsync<ApiResponse>(new ApiResponse { 
                    ErrorMessage = e.Message,
                    IsError = true
                
                });
            }
            catch(ValidationException e)
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsJsonAsync<ApiResponse>(new ApiResponse { 
                    ErrorMessage = e.Message,
                    IsError = true });
            }
           

        }
    }
}
