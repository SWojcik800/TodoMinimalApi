using MediatR;
using TodoMinimalApi.Contexts;
using MapsterMapper;
using TodoMinimalApi.Entities.Todos;
using TodoMinimalApi.Features.Todos.Dtos;
using TodoMinimalApi.Features.Authorization.Services;
using TodoMinimalApi.DataAccess.Repositories;
using TodoMinimalApi.Entities.Account;
using Microsoft.AspNetCore.Identity;

namespace TodoMinimalApi.Features.Todos
{
    public class CreateTodo : IRequest
    {
        public CreateTodoDto CreateDto { get; set; }
      
    }

    public class CreateTodoHandler : IRequestHandler<CreateTodo>
    {
        private readonly IRepository<Todo, long> _todosRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ISessionService _session;
        public CreateTodoHandler(
            IRepository<Todo, long> todosRepository,
            UserManager<User> userManager,
            IMapper mapper,
            ISessionService session
            )
        {
           _todosRepository = todosRepository;
           _userManager = userManager;
           _mapper = mapper;
           _session = session;
        }

        public async Task<Unit> Handle(CreateTodo request, CancellationToken cancellationToken)
        {
            var currentUserId = _session.GetUserId();

            var todoEntity = _mapper.Map<Todo>(request.CreateDto);
            var todoUser = await _userManager.FindByIdAsync(currentUserId);

            todoEntity.User = todoUser;

            await _todosRepository.InsertAsync(todoEntity, cancellationToken);
            await _todosRepository.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
