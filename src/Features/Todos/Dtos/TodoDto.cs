using TodoMinimalApi.Common;
using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.Features.Todos.Dtos
{
    public class TodoDto : EntityDto<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoState TodoState { get; set; }
    }
}
