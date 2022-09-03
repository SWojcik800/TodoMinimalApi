using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.Contexts
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public virtual DbSet<Todo> Todos { get; set; }
    }
}
