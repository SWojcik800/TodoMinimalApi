using FluentValidation;
using TodoMinimalApi.Features.Authorization.Dtos;

namespace TodoMinimalApi.Features.Authorization.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress()
                .NotEmpty()
                .WithMessage("This field is required");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("This field is required");
            RuleFor(x => x.Password)
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
                .WithMessage("Password must contain at least 8 characters, 1 uppercase, 1 lowercase letter and 1 number");
            RuleFor(x => x.ConfirmPassword).NotEmpty()
                .WithMessage("This field is required");
            RuleFor(x => x.Password)
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Password does not match");
              
        }
    }
}
