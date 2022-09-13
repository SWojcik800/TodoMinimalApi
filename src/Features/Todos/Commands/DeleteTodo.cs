using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.DataAccess.Repositories;
using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.Features.Todos
{
    public class DeleteTodo : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteTodoHandler : IRequestHandler<DeleteTodo>
    {
        private readonly IRepository<Todo, long> _todosRepository;
        public DeleteTodoHandler(IRepository<Todo, long> todosRepository)
        {
            _todosRepository = todosRepository;
        }
        public async Task<Unit> Handle(DeleteTodo request, CancellationToken cancellationToken)
        {
           var todoToDelete = await _todosRepository.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id);

           if (todoToDelete is null)
               throw new NotFoundException("Todo not found");

            _todosRepository.Delete(todoToDelete);
            await _todosRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
