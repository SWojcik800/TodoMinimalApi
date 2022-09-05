using MediatR;
using TodoMinimalApi.Contexts;
using MapsterMapper;
using TodoMinimalApi.Entities.Todos;
using TodoMinimalApi.Features.Todos.Dtos;
using TodoMinimalApi.Features.Authorization.Services;

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
        private readonly ISessionService _session;
        public CreateTodoHandler(
            TodoContext context,
            IMapper mapper,
            ISessionService session
            )
        {
           _context = context;
           _mapper = mapper;
           _session = session;
        }

        public async Task<Unit> Handle(CreateTodo request, CancellationToken cancellationToken)
        {
            var currentUserId = _session.GetUserId();

            var todoEntity = _mapper.Map<Todo>(request.CreateDto);
            var todoUser = await _context.Users.FindAsync(currentUserId);

            todoEntity.User = todoUser;

            await _context.Todos.AddAsync(todoEntity);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
