using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public class GetTodo : IRequest<TodoDto>
    {
        public long Id { get; set; }
    }
    public class GetTodoHandler : IRequestHandler<GetTodo, TodoDto>
    {
        private readonly TodoContext _context;
        public GetTodoHandler(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoDto> Handle(GetTodo request, CancellationToken cancellationToken)
        {
            var todo = await _context.Todos
                .AsNoTracking()
                .ProjectToType<TodoDto>()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (todo is null)
                throw new NotFoundException("Todo not found");

            return todo;
        }
    }
}
