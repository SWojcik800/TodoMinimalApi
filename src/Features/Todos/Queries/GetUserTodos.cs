using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Contexts;
using TodoMinimalApi.Features.Authorization.Services;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos.Queries
{
    public class GetUserTodos : IRequest<List<TodoDto>>
    {
    }

    public class GetUserTodosHandler : IRequestHandler<GetUserTodos, List<TodoDto>>
    {
        private readonly TodoContext _context;
        private readonly ISessionService _session;
        public GetUserTodosHandler(
         TodoContext context,
         ISessionService session)
        {
            _context = context;
            _session = session;
        }
        public async Task<List<TodoDto>> Handle(GetUserTodos request, CancellationToken cancellationToken)
        {
            var todos = await _context.Todos
             .AsNoTracking()
             .Where(t => t.User.Id == _session.GetUserId())
             .ProjectToType<TodoDto>()
             .ToListAsync(cancellationToken);

            return todos;
        }
    }

   
  
    
}
