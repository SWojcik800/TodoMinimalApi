using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoMinimalApi.Common.Exceptions;
using TodoMinimalApi.Entities.Account;
using TodoMinimalApi.Features.Authorization.Dtos;

namespace TodoMinimalApi.Features.Authorization.Queries
{
    public class AuthorizeUser : IRequest<AuthorizeResponseDto>
    {
        public LoginDto LoginDto { get; set; }
    }

    public class AuthorizeUserHandler : IRequestHandler<AuthorizeUser, AuthorizeResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtTokenSettings _tokenSettings;
        public AuthorizeUserHandler(
            UserManager<User> userManager,
            IConfiguration configuration,
            JwtTokenSettings tokenSettings)
        {
            _userManager = userManager;
            _configuration = configuration;
            _tokenSettings = tokenSettings;
        }
        public async Task<AuthorizeResponseDto> Handle(AuthorizeUser request, CancellationToken cancellationToken)
        {
            var loginDto = request.LoginDto;
            var user = await _userManager.FindByNameAsync(loginDto.Login);

            if (user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new UnauthorizedException("Invalid login attempt");
            }


            var userRoles = await _userManager.GetRolesAsync(user);



            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("Roles", userRoles.ToString())
                }),
                Expires = DateTime.Now.AddHours(3),
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.Audience,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthorizeResponseDto
            {
                Token = tokenString,
                Email = user.Email,
                Roles = userRoles.ToList()

            };
        }
    }
}
