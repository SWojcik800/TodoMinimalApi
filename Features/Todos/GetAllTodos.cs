using MediatR;
using TodoMinimalApi.Contexts;
using System.Collections.Generic;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace TodoMinimalApi.Features.Todos
{
    public class GetAllTodos : IRequest<List<TodoDto>>
    {
        public int SkipCount { get; set; } = 0;
        public int MaxResultCount { get; set; } = 10;
    }

    public class GetAllTodosHandler : IRequestHandler<GetAllTodos, List<TodoDto>>
    {
        private readonly TodoContext _context;
        public GetAllTodosHandler(TodoContext context)
        {
           _context = context;
        }
        public async Task<List<TodoDto>> Handle(GetAllTodos request, CancellationToken cancellationToken)
        {
           var todos = await _context.Todos
                .Skip(request.SkipCount)
                .Take(request.MaxResultCount)
                .ProjectToType<TodoDto>()
                .ToListAsync();

            return todos;
        }
    }
}
