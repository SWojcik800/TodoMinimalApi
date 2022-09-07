using FluentValidation;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos.Validators
{
    public class UpdateTodoValidator : AbstractValidator<UpdateTodoDto>
    {
        public UpdateTodoValidator()
        {
            RuleFor(x => x.Id).NotNull()
                .WithMessage("Id cannot be null");
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name cannot be empty");
            
        }
    }
}
