using Serdiuk.ToDoList.Application.Dtos.ToDo;
using Serdiuk.ToDoList.Domain;

namespace Serdiuk.ToDoList.Application.Common.Interfaces
{
    public interface IToDoItemService
    {
        Task<IEnumerable<ToDoItem>> GetCompleteToDoAsync(Guid userId);
        Task<IEnumerable<ToDoItem>> GetIncompleteToDoAsync(Guid userId);
        Task<IEnumerable<ToDoItem>> GetAll(Guid userId);
        Task<ToDoItem> GetById(Guid id, Guid userId);
        Task<Guid> AddToDoAsync(CreateToDoItemDto todo, Guid userId);
        Task<Guid> DeleteToDoAsync(DeleteToDoItemDto todo, Guid userId);
        Task<bool> UpdateDoneAsync(UpdateDoneToDoItemDto todo, Guid userId);
        Task<bool> UpdateToDoAsync(UpdateToDoItemDto todo, Guid userId);
    }
}
