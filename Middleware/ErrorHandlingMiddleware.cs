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
            catch (NotFoundException e)
            {

                context.Response.StatusCode = 404;
            }
            catch (BadRequestException e)
            {
                context.Response.StatusCode = 400;
            }
            
        }
    }
}
