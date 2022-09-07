
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Common.Response;
using TodoMinimalApi.Features.Todos.Dtos;
using TodoMinimalApi.Features.Todos.Validators;

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
                var todos = await mediator.Send(new GetUserTodos()
                {
                    SkipCount = skipCount,
                    MaxResultCount = maxResultCount,
                });

                return new ApiResponse<PaginatedResponse<TodoDto>>(todos);
            }).Produces<PaginatedResponse<TodoDto>>();

            app.MapGet("/todos/get", async ([FromServices] IMediator mediator, [FromQuery] long id) =>
            {
                var todo = await mediator.Send(new GetTodo() { Id = id });
                return new ApiResponse<TodoDto>(todo);
               
            }).Produces<TodoDto>();


            app.MapPost("/todos/create", async (
                [FromServices] IMediator mediator,
                [FromServices] CreateTodoValidator validator,
                CreateTodoDto dto) =>
            {
                await validator.ValidateAndThrowAsync(dto);

                await mediator.Send(new CreateTodo()
                {
                    CreateDto = dto
                });

                return new ApiResponse();
            });

            app.MapPut("/todos/update", async (
                [FromServices] IMediator mediator,
                [FromServices] UpdateTodoValidator validator,
                UpdateTodoDto updateDto) =>
            {
                await validator.ValidateAndThrowAsync(updateDto);

                await mediator.Send(new UpdateTodo()
                {
                    Dto = updateDto
                });

                return new ApiResponse();
            });

            app.MapDelete("/todos/delete", async ([FromServices] IMediator mediator, long id) =>
                {
                    await mediator.Send(new DeleteTodo()
                    {
                        Id = id
                    });

                    return new ApiResponse();
                });
           
        }
    }
}
