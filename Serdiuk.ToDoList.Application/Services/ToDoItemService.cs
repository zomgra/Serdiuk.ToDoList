using Microsoft.EntityFrameworkCore;
using Serdiuk.ToDoList.Application.Common.Interfaces;
using Serdiuk.ToDoList.Application.Dtos.ToDo;
using Serdiuk.ToDoList.Application.Exceptions;
using Serdiuk.ToDoList.Domain;

namespace Serdiuk.ToDoList.Application.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IAppDbContext _context;

        public ToDoItemService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddToDoAsync(CreateToDoItemDto todo, string userId)
        {
            var todoItem = new ToDoItem
            {
                Date = DateTime.UtcNow,
                IsDone = false,
                Id = Guid.NewGuid(),
                Title = todo.Title,
                UserId = userId,
            };
            _context.ToDoItems.Add(todoItem);
            await _context.SaveChangesAsync(CancellationToken.None);
            return todoItem.Id;
        }

        public async Task<Guid> DeleteToDoAsync(DeleteToDoItemDto todo, string userId)
        {
            var entity = await _context.ToDoItems.FirstOrDefaultAsync(x=>x.Id==todo.Id);
            if (entity == null)
                throw new TodoNotFoundException("This todo not found, try again");

            if (entity.UserId != userId)
                throw new TodoNotEnoughPermissionsException("You do not have permissions to edit this Todo");

            _context.ToDoItems.Remove(entity);
                await _context.SaveChangesAsync(CancellationToken.None);
            return entity.Id;
        }

        public async Task<IEnumerable<ToDoItem>> GetAll(string userId)
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> GetById(Guid id, string userId)
        {
            var entity = await _context.ToDoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (entity == null)
                throw new TodoNotFoundException("This todo not found, try again");

            if (entity.UserId != userId)
                throw new TodoNotEnoughPermissionsException("You do not have permissions to get this Todo");
            return entity;
        }

        public async Task<IEnumerable<ToDoItem>> GetCompleteToDoAsync(string userId)
        {
            return await _context.ToDoItems.Where(x => x.UserId==userId)
                .Where(x=>x.IsDone)
                .ToListAsync();
        }

        public async Task<IEnumerable<ToDoItem>> GetIncompleteToDoAsync(string userId)
        {
            return await _context.ToDoItems.Where(x => x.UserId == userId)
                .Where(x => !x.IsDone)
                .ToListAsync();
        }

        public async Task<bool> UpdateDoneAsync(UpdateDoneToDoItemDto todo, string userId)
        {
            var entity = await _context.ToDoItems.FirstOrDefaultAsync(x=>x.Id==todo.Id);


            if (entity == null)
                throw new TodoNotFoundException("This todo not found, try again");

            if (entity.UserId != userId)
                throw new TodoNotEnoughPermissionsException("You do not have permissions to edit this Todo");

            if (entity.IsDone == todo.SetDone)
                throw new TodoAlreadySetDoneException();

            entity.IsDone = !entity.IsDone;
            return await _context.SaveChangesAsync(CancellationToken.None) == 1;
        }

        public async Task<bool> UpdateToDoAsync(UpdateToDoItemDto todo, string userId)
        {
            var entity = await _context.ToDoItems.FirstOrDefaultAsync(x => x.Id == todo.Id);
            if (entity == null)
                throw new TodoNotFoundException("This todo not found, try again");

            if (entity.UserId != userId)
                throw new TodoNotEnoughPermissionsException("You do not have permissions to edit this Todo");

            entity.Title = todo.NewTitle;
            return await _context.SaveChangesAsync(CancellationToken.None) == 1;
        }
    }
}
