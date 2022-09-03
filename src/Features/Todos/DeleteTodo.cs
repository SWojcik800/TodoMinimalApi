using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;

namespace TodoMinimalApi.Features.Todos
{
    public class DeleteTodo : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteTodoHandler : IRequestHandler<DeleteTodo>
    {
        private readonly TodoContext _context;
        public DeleteTodoHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteTodo request, CancellationToken cancellationToken)
        {
           var todoToDelete = await _context.Todos.FirstOrDefaultAsync(x => x.Id == request.Id);

           if (todoToDelete is null)
               throw new NotFoundException("Todo not found");

            _context.Remove(todoToDelete);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
