using MediatR;
using TodoMinimalApi.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Features.Todos.Dtos;
using TodoMinimalApi.Common.Response;
using TodoMinimalApi.Features.Authorization.Services;
using TodoMinimalApi.DataAccess.Repositories;
using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.Features.Todos
{
    public class GetPaginatedUserTodos : IRequest<PaginatedResponse<TodoDto>>
    {
        public int SkipCount { get; set; } = 0;
        public int MaxResultCount { get; set; } = 10;
    }

    public class GetPaginatedUserTodosHandler : IRequestHandler<GetPaginatedUserTodos, PaginatedResponse<TodoDto>>
    {
        private readonly IRepository<Todo, long> _todosRepository;
        private readonly ISessionService _session;
        public GetPaginatedUserTodosHandler(
            IRepository<Todo, long> todosRepository,
             ISessionService session)
        {
           _todosRepository = todosRepository;
           _session = session;
        }
        public async Task<PaginatedResponse<TodoDto>> Handle(GetPaginatedUserTodos request, CancellationToken cancellationToken)
        {
            
           var todos = await _todosRepository.GetAll()
                .AsNoTracking()
                .Skip(request.SkipCount)
                .Take(request.MaxResultCount)
                .Where(t => t.User.Id == _session.GetUserId())
                .ProjectToType<TodoDto>()
                .ToListAsync();

            var totalCount = await _todosRepository.GetAll()
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
