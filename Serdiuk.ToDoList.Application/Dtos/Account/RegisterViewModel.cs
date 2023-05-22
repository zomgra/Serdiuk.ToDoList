using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Serdiuk.ToDoList.Application.Dtos.Account
{
    public class RegisterViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, PasswordPropertyText]
        public string Password { get; set; }
        [Required, PasswordPropertyText, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
