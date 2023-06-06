using NodaTime;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Serdiuk.ToDoList.Domain
{
    public class ToDoItem
    {
        [Required, Key]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required, MaxLength(256)]
        public string Title { get; set; } = null!;
        public bool IsDone { get; set; }
        public DateTime Date { get; set; }
    }
}
