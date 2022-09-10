
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Common.Response;
using TodoMinimalApi.Features.Todos.Dtos;
using TodoMinimalApi.Features.Todos.Queries;
using TodoMinimalApi.Features.Todos.Validators;

namespace TodoMinimalApi.Features.Todos
{
    /// <summary>
    /// Registers endpoints for todos
    /// </summary>
    public static class TodoEndpoints
    {
        public static void AddTodo(this WebApplication app)
        {

            app.MapGet("/todos/getUserPaginatedTodos", async (
                [FromServices] IMediator mediator,
                [FromQuery] int? skipCount,
                [FromQuery] int? maxResultCount,
                CancellationToken cancellationToken) =>
            {
                var request = new GetPaginatedUserTodos();

                if (skipCount is not null)
                    request.SkipCount = (int)skipCount;
                if (maxResultCount is not null)
                    request.MaxResultCount = (int)maxResultCount;

                var todos = await mediator.Send(request, cancellationToken);

                return new ApiResponse<PaginatedResponse<TodoDto>>(todos);
            }).Produces<PaginatedResponse<TodoDto>>();

            app.MapGet("/todos/getUserTodos", async (
                [FromServices] IMediator mediator,
                 CancellationToken cancellationToken) =>
            {
                await Task.Delay(5000);
                var todos = await mediator.Send(new GetUserTodos(), cancellationToken);

                return new ApiResponse<List<TodoDto>>(todos);
            }).Produces<PaginatedResponse<TodoDto>>();


            app.MapGet("/todos/get", async (
                [FromServices] IMediator mediator,
                [FromQuery] long id,
                 CancellationToken cancellationToken) =>
            {
                var todo = await mediator.Send(new GetTodo() { Id = id }, cancellationToken);
                return new ApiResponse<TodoDto>(todo);
               
            }).Produces<TodoDto>();


            app.MapPost("/todos/create", async (
                [FromServices] IMediator mediator,
                [FromServices] CreateTodoValidator validator,
                CreateTodoDto dto,
                CancellationToken cancellationToken) =>
            {
                await validator.ValidateAndThrowAsync(dto);

                await mediator.Send(new CreateTodo()
                {
                    CreateDto = dto
                }, cancellationToken);

                return new ApiResponse();
            });

            app.MapPut("/todos/update", async (
                [FromServices] IMediator mediator,
                [FromServices] UpdateTodoValidator validator,
                UpdateTodoDto updateDto,
                CancellationToken cancellationToken) =>
            {
                await validator.ValidateAndThrowAsync(updateDto);

                await mediator.Send(new UpdateTodo()
                {
                    Dto = updateDto
                }, cancellationToken);

                return new ApiResponse();
            });

            app.MapPut("/todos/changeStatus", async (
                [FromServices] IMediator mediator,
                [FromServices] ChangeTodoStateValidator validator,
                ChangeTodoStatusDto changeStatusDto,
                CancellationToken cancellationToken) =>
            {
                await validator.ValidateAndThrowAsync(changeStatusDto);

                await mediator.Send(new ChangeTodoStatus()
                {
                    Id = changeStatusDto.Id,
                    TodoState = changeStatusDto.TodoState
                }, cancellationToken);

                return new ApiResponse();
            });

            app.MapDelete("/todos/delete", async (
                [FromServices] IMediator mediator,
                long id,
                CancellationToken cancellationToken) =>
                {
                    await mediator.Send(new DeleteTodo()
                    {
                        Id = id
                    }, cancellationToken);

                    return new ApiResponse();
                });
           
        }
    }
}
