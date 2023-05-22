using Microsoft.EntityFrameworkCore;
using Serdiuk.ToDoList.Domain;

namespace Serdiuk.ToDoList.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
