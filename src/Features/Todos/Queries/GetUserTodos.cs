using MediatR;
using TodoMinimalApi.Contexts;
using System.Collections.Generic;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Features.Todos.Dtos;
using TodoMinimalApi.Common.Response;
using TodoMinimalApi.Features.Authorization.Services;

namespace TodoMinimalApi.Features.Todos
{
    public class GetUserTodos : IRequest<PaginatedResponse<TodoDto>>
    {
        public int SkipCount { get; set; }     
        public int MaxResultCount { get; set; }
    }

    public class GetUserTodosHandler : IRequestHandler<GetUserTodos, PaginatedResponse<TodoDto>>
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
        public async Task<PaginatedResponse<TodoDto>> Handle(GetUserTodos request, CancellationToken cancellationToken)
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
