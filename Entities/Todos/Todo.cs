using TodoMinimalApi.Common;

namespace TodoMinimalApi.Entities.Todos
{
    public enum TodoState {
        New,
        InProgress,
        Done
    };
    public class Todo : Entity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoState TodoState { get; set; }
    }
}
