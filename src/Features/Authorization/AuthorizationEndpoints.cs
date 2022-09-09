
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalApi.Common.Response;
using TodoMinimalApi.Features.Authorization.Commands;
using TodoMinimalApi.Features.Authorization.Dtos;
using TodoMinimalApi.Features.Authorization.Queries;
using TodoMinimalApi.Features.Authorization.Validators;

namespace TodoMinimalApi.Features.Authorization
{
    /// <summary>
    /// Registers endpoints for authorization
    /// </summary>
    public static class AuthorizationEndpoints
    {
        public static void AddAppAuthorization(this WebApplication app)
        {
            app.MapPost("/account/register", async (
                [FromServices] IMediator mediator,
                [FromServices] RegisterUserValidator validator,
                [FromBody] RegisterUserDto dto) =>
            {
                await validator.ValidateAndThrowAsync(dto);

                await mediator.Send(new RegisterUser
                {
                    Email = dto.Email,
                    Password = dto.Password,
                    ConfirmPassword = dto.ConfirmPassword
                });

                return new ApiResponse();
            }).AllowAnonymous();

            app.MapPost("/account/authorize", async (
                [FromServices] IMediator mediator,
                [FromBody] LoginDto dto) =>
            {
                var result = await mediator.Send(new AuthorizeUser
                {
                    LoginDto = new LoginDto(dto.Login, dto.Password)
                });

                return new ApiResponse<AuthorizeResponseDto>(result);
            }).AllowAnonymous()
              .Produces<AuthorizeResponseDto>();
        }

    }
}
