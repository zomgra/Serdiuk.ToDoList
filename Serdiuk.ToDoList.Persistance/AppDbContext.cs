using Microsoft.EntityFrameworkCore;
using Serdiuk.ToDoList.Application.Common.Interfaces;
using Serdiuk.ToDoList.Domain;

namespace Serdiuk.ToDoList.Persistance
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
