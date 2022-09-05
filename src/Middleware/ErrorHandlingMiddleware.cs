using TodoMinimalApi.Common.Exceptions;

namespace TodoMinimalApi.Middleware
{
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
                await context.Response.WriteAsync(e.Message);
            }
           

        }
    }
}
