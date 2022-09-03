
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TodoMinimalApi.Features.Todos
{
    public static class TodoEndpoints
    {
        public static void AddTodo(this WebApplication app)
        {

            app.MapGet("/todos", async ([FromServices] IMediator mediator) =>
            {
                var todos = await mediator.Send(new GetAllTodos()
                {
                    SkipCount = 0,
                    MaxResultCount = 10,
                });

                return todos;
            });


            app.MapPost("/todos/create", async ([FromServices] IMediator mediator, CreateTodoDto dto) =>
            {
                var todos = await mediator.Send(new CreateTodo()
                {
                    CreateDto = dto
                });

                return todos;
            });
           
        }
    }
}
