using System.ComponentModel.DataAnnotations;

namespace Intern.ViewModels
{
    public class SignInRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
