using FluentValidation;
using TodoMinimalApi.Features.Todos.Dtos;

namespace TodoMinimalApi.Features.Todos.Validators
{
    public class ChangeTodoStateValidator : AbstractValidator<ChangeTodoStatusDto>
    {
        public ChangeTodoStateValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.TodoState).NotNull();
        }
    }
}
