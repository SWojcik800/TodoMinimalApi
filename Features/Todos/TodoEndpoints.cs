
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public static class TodoEndpoints
    {
        public static void AddTodo(this WebApplication app)
        {

            app.MapGet("/todos", async ([FromServices] IMediator mediator,
                [FromQuery] int skipCount,
                [FromQuery] int maxResultCount) =>
            {
                var todos = await mediator.Send(new GetAllTodos()
                {
                    SkipCount = skipCount,
                    MaxResultCount = maxResultCount,
                });

                return todos;
            });

            app.MapGet("/todos/get", async ([FromServices] IMediator mediator, [FromQuery] long id) =>
            {
                var todo = await mediator.Send(new GetTodo() { Id = id });
                return todo;
            });


            app.MapPost("/todos/create", async ([FromServices] IMediator mediator, CreateTodoDto dto) =>
            {
                var todos = await mediator.Send(new CreateTodo()
                {
                    CreateDto = dto
                });

                return todos;
            });

            app.MapDelete("/todos/delete", async ([FromServices] IMediator mediator, long id) =>
                {
                    await mediator.Send(new DeleteTodo()
                    {
                        Id = id
                    });
                });
           
        }
    }
}
