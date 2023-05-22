using System.ComponentModel.DataAnnotations;

namespace Serdiuk.ToDoList.Application.Dtos.ToDo
{
    public class UpdateDoneToDoItemDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
