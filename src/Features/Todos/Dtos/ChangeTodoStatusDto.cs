using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.Features.Todos.Dtos
{
    public class ChangeTodoStatusDto
    {
        public long Id { get; set; }
        public TodoState TodoState { get; set; }
    }
}
