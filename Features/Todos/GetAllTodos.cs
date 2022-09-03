using MediatR;
using TodoMinimalApi.Contexts;
using System.Collections.Generic;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public class GetAllTodos : IRequest<List<TodoDto>>
    {
        public int SkipCount { get; set; }     
        public int MaxResultCount { get; set; }
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
                .AsNoTracking()
                .Skip(request.SkipCount)
                .Take(request.MaxResultCount)
                .ProjectToType<TodoDto>()
                .ToListAsync();

            return todos;
        }
    }
}
