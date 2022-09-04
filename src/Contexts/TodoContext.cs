using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Entities.Account;
using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.Contexts
{
    public class TodoContext : IdentityDbContext<User>
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public virtual DbSet<Todo> Todos { get; set; }
    }
}
