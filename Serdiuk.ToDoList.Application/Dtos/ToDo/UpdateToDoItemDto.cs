using System.ComponentModel.DataAnnotations;

namespace Serdiuk.ToDoList.Application.Dtos.ToDo
{
    public class UpdateToDoItemDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required, MaxLength(256)]
        public string NewTitle { get; set; }
    }
}
