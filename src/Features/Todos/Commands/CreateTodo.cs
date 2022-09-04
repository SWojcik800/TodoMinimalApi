using MediatR;
using TodoMinimalApi.Contexts;
using MapsterMapper;
using TodoMinimalApi.Entities.Todos;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos
{
    public class CreateTodo : IRequest
    {
        public CreateTodoDto CreateDto { get; set; }
    }

    public class CreateTodoHandler : IRequestHandler<CreateTodo>
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;
        public CreateTodoHandler(
            TodoContext context,
            IMapper mapper
            )
        {
           _context = context;
           _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateTodo request, CancellationToken cancellationToken)
        {
            var todoEntity = _mapper.Map<Todo>(request.CreateDto);

            await _context.Todos.AddAsync(todoEntity);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
