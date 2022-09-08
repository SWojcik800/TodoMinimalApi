using MediatR;
using TodoMinimalApi.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Features.Todos.Dtos;
using TodoMinimalApi.Common.Response;
using TodoMinimalApi.Features.Authorization.Services;

namespace TodoMinimalApi.Features.Todos
{
    public class GetPaginatedUserTodos : IRequest<PaginatedResponse<TodoDto>>
    {
        public int SkipCount { get; set; } = 0;
        public int MaxResultCount { get; set; } = 10;
    }

    public class GetPaginatedUserTodosHandler : IRequestHandler<GetPaginatedUserTodos, PaginatedResponse<TodoDto>>
    {
        private readonly TodoContext _context;
        private readonly ISessionService _session;
        public GetPaginatedUserTodosHandler(
            TodoContext context,
            ISessionService session)
        {
           _context = context;
           _session = session;
        }
        public async Task<PaginatedResponse<TodoDto>> Handle(GetPaginatedUserTodos request, CancellationToken cancellationToken)
        {
            
           var todos = await _context.Todos
                .AsNoTracking()
                .Skip(request.SkipCount)
                .Take(request.MaxResultCount)
                .Where(t => t.User.Id == _session.GetUserId())
                .ProjectToType<TodoDto>()
                .ToListAsync();

            var totalCount = await _context.Todos
                .Where(t => t.User.Id == _session.GetUserId())
                .CountAsync();

            return new PaginatedResponse<TodoDto>
            {
                TotalCount = totalCount,
                Items = todos
            };
        }
    }
}
