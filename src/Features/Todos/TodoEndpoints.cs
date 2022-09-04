
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Common.Response;
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
            }).Produces<PaginatedResponse<TodoDto>>();

            app.MapGet("/todos/get", async ([FromServices] IMediator mediator, [FromQuery] long id) =>
            {
                var todo = await mediator.Send(new GetTodo() { Id = id });
                return todo;
            }).Produces<TodoDto>();


            app.MapPost("/todos/create", async ([FromServices] IMediator mediator, CreateTodoDto dto) =>
            {
                await mediator.Send(new CreateTodo()
                {
                    CreateDto = dto
                });
            });

            app.MapPut("/todos/update", async ([FromServices] IMediator mediator, UpdateTodoDto updateDto) =>
            {
                await mediator.Send(new UpdateTodo()
                {
                    Dto = updateDto
                });
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
