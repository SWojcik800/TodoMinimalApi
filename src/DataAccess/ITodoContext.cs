using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Entities.Todos;

namespace TodoMinimalApi.DataAccess
{
    public interface ITodoContext
    {
        public DbSet<Todo> Todos { get; set; }

    }
}
