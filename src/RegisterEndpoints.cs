using TodoMinimalApi.Features.Authorization;
using TodoMinimalApi.Features.Todos;

namespace TodoMinimalApi
{
    public static class RegisterEndpoints
    {
        public static void AddApplicationEndpoints(this WebApplication app)
        {
            app.AddAppAuthorization();
            app.AddTodo();
        }

    }
}
