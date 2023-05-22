using System.ComponentModel.DataAnnotations;

namespace Serdiuk.ToDoList.Application.Dtos.ToDo
{
    public class CreateToDoItemDto
    {
        [Required, MaxLength(256)]
        public string Title { get; set; } = null!;
    }
}
