using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.DataAccess.Repositories;
using TodoMinimalApi.Entities.Todos;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public class UpdateTodo : IRequest
    {
        public UpdateTodoDto Dto { get; set; }
    }

    public class UpdateTodoHandler : IRequestHandler<UpdateTodo>
    {
        private readonly IRepository<Todo, long> _todosRepository;
        public UpdateTodoHandler(IRepository<Todo, long> todosRepository)
        {
            _todosRepository = todosRepository;
        }
        public async Task<Unit> Handle(UpdateTodo request, CancellationToken cancellationToken)
        {
            var todoToUpdate = await _todosRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == request.Dto.Id);

            if (todoToUpdate is null)
                throw new NotFoundException("Todo not found");

            var dto = request.Dto;
            todoToUpdate.Name = dto.Name;
            todoToUpdate.Description = dto.Description;
            todoToUpdate.TodoState = dto.TodoState;

            _todosRepository.Update(todoToUpdate);
            await _todosRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
