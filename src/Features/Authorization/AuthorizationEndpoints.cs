
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Features.Authorization.Commands;
using TodoMinimalApi.Features.Authorization.Dtos;
using TodoMinimalApi.Features.Authorization.Queries;

namespace TodoMinimalApi.Features.Authorization
{
    public static class AuthorizationEndpoints
    {
        public static void AddAppAuthorization(this WebApplication app)
        {
            app.MapPost("/account/register", async ([FromServices] IMediator mediator,
                [FromBody] RegisterUserDto dto) =>
            {
                await mediator.Send(new RegisterUser
                {
                    Email = dto.Email,
                    Password = dto.Password,
                    ConfirmPassword = dto.ConfirmPassword
                });
            }).AllowAnonymous();

            app.MapPost("/account/authorize", async ([FromServices] IMediator mediator,
                [FromBody] LoginDto dto) =>
            {
                var result = await mediator.Send(new AuthorizeUser
                {
                    LoginDto = new LoginDto(dto.Login, dto.Password)
                });

                return result;
            }).AllowAnonymous()
              .Produces<AuthorizeResponseDto>();
        }

    }
}
