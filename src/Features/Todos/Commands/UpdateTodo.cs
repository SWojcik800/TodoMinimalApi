using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public class UpdateTodo : IRequest
    {
        public UpdateTodoDto Dto { get; set; }
    }

    public class UpdateTodoHandler : IRequestHandler<UpdateTodo>
    {
        private readonly TodoContext _context;
        public UpdateTodoHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateTodo request, CancellationToken cancellationToken)
        {
            var todoToUpdate = await _context.Todos.FirstOrDefaultAsync(x => x.Id == request.Dto.Id);

            if (todoToUpdate is null)
                throw new NotFoundException("Todo not found");

            var dto = request.Dto;
            todoToUpdate.Name = dto.Name;
            todoToUpdate.Description = dto.Description;
            todoToUpdate.TodoState = dto.TodoState;

            _context.Todos.Update(todoToUpdate);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
