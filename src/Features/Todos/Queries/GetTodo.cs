using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.DataAccess.Repositories;
using TodoMinimalApi.Entities.Todos;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public class GetTodo : IRequest<TodoDto>
    {
        public long Id { get; set; }
    }
    public class GetTodoHandler : IRequestHandler<GetTodo, TodoDto>
    {
        private readonly IRepository<Todo, long> _todosRepository;
        public GetTodoHandler(IRepository<Todo, long> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<TodoDto> Handle(GetTodo request, CancellationToken cancellationToken)
        {
            var todo = await _todosRepository.GetAll()
                .AsNoTracking()
                .ProjectToType<TodoDto>()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (todo is null)
                throw new NotFoundException("Todo not found");

            return todo;
        }
    }
}
