using System.ComponentModel.DataAnnotations;

namespace Intern.ViewModels.Authen
{
    public class SignInRequest
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string userPass { get; set; } = null!;
    }
}
