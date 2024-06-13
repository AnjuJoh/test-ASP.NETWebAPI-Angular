using System.ComponentModel.DataAnnotations;

namespace Event.Models
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
     
        [EmailAddress]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
       
        public string Password { get; set; }
    }
}
