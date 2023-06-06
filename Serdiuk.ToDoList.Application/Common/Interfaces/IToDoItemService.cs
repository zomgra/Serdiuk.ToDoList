using Serdiuk.ToDoList.Application.Dtos.ToDo;
using Serdiuk.ToDoList.Domain;

namespace Serdiuk.ToDoList.Application.Common.Interfaces
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetCompleteToDoAsync(string userId);
        Task<IEnumerable<ToDoItem>> GetIncompleteToDoAsync(string userId);
        Task<IEnumerable<ToDoItem>> GetAll(string userId);
        Task<ToDoItem> GetById(Guid id, string userId);
        Task<Guid> AddToDoAsync(CreateToDoItemDto todo, string userId);
        Task<Guid> DeleteToDoAsync(DeleteToDoItemDto todo, string userId);
        Task<bool> UpdateDoneAsync(UpdateDoneToDoItemDto todo, string userId);
        Task<bool> UpdateToDoAsync(UpdateToDoItemDto todo, string userId);
    }
}
