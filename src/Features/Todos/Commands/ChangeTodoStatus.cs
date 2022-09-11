using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.DataAccess.Repositories;
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
        private readonly IRepository<Todo, long> _todosRepository;
        public ChangeTodoStatusHandler(IRepository<Todo, long> todosRepository)
        {
            _todosRepository = todosRepository;
        }
        public async Task<Unit> Handle(ChangeTodoStatus request, CancellationToken cancellationToken)
        {
            var todoToUpdate = await _todosRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (todoToUpdate is null)
                throw new NotFoundException("Todo not found");

            
            todoToUpdate.TodoState = request.TodoState;

            _todosRepository.Update(todoToUpdate);
            await _todosRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
