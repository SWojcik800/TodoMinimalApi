using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Entities.Account;

namespace TodoMinimalApi.Features.Authorization.Commands
{
    public class RegisterUser : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterUserHandler : IRequestHandler<RegisterUser>
    {
        private readonly UserManager<User> _userManager;
        public RegisterUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Unit> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
                throw new BadRequestException("Passwords does not match");

            var userExists = await _userManager.FindByNameAsync(request.Email);

            if(userExists is not null)
                throw new BadRequestException("User exists");

            var result = await _userManager.CreateAsync(new User
            {
                Email = request.Email,
                UserName = request.Email
            }, request.Password);

            return Unit.Value;
        }
    }
}
