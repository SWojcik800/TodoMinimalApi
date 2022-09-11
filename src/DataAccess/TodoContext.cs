using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.DataAccess;
using TodoMinimalApi.Entities.Account;
using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.Contexts
{
    public class TodoContext : IdentityDbContext<User>, ITodoContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Todo>()
                .HasQueryFilter(x => !x.IsDeleted);

            base.OnModelCreating(builder);
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
