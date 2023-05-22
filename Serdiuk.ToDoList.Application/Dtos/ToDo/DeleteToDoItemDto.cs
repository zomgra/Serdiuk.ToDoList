using System.ComponentModel.DataAnnotations;

namespace Serdiuk.ToDoList.Application.Dtos.ToDo
{
    public class DeleteToDoItemDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
