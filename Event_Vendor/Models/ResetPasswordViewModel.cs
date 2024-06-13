using System.ComponentModel.DataAnnotations;

namespace Event.Models
{
    public class ResetPasswordViewModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string token { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
