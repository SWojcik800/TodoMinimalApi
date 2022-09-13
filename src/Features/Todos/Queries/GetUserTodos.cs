using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.DataAccess.Repositories;
using TodoMinimalApi.Entities.Todos;
using TodoMinimalApi.Features.Authorization.Services;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos.Queries
{
    public class GetUserTodos : IRequest<List<TodoDto>>
    {
    }

    public class GetUserTodosHandler : IRequestHandler<GetUserTodos, List<TodoDto>>
    {
        private readonly IRepository<Todo, long> _todosRepository;
        private readonly ISessionService _session;
        public GetUserTodosHandler(
         IRepository<Todo, long> todosRepository,
         ISessionService session)
        {
            _todosRepository = todosRepository;
            _session = session;
        }
        public async Task<List<TodoDto>> Handle(GetUserTodos request, CancellationToken cancellationToken)
        {
            var todos = await _todosRepository.GetAll()
             .AsNoTracking()
             .Where(t => t.User.Id == _session.GetUserId())
             .ProjectToType<TodoDto>()
             .ToListAsync(cancellationToken);

            return todos;
        }
    }

   
  
    
}
