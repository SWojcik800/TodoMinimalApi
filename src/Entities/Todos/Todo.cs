using System.ComponentModel.DataAnnotations.Schema;
using TodoMinimalApi.Common;
using TodoMinimalApi.Entities.Account;

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
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
