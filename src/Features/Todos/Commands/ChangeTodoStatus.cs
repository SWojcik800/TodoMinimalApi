using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.Entities.Todos;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public class ChangeTodoStatus : IRequest
    {
        public long Id { get; set; }
        public TodoState TodoState { get; set; }
    }

    public class ChangeTodoStatusHandler : IRequestHandler<ChangeTodoStatus>
    {
        private readonly TodoContext _context;
        public ChangeTodoStatusHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(ChangeTodoStatus request, CancellationToken cancellationToken)
        {
            var todoToUpdate = await _context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (todoToUpdate is null)
                throw new NotFoundException("Todo not found");

            
            todoToUpdate.TodoState = request.TodoState;

            _context.Todos.Update(todoToUpdate);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
