using System.ComponentModel.DataAnnotations;

namespace Event.Models
{
    public class RegistrationViewModel
    {

        public int Id { get; set; }
        [RegularExpression("^[A-Z][a-z]*$",
        ErrorMessage = "Please enter valid name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([gmail|yahoo]+\\.)+[com|in|ca]{2,6}$",
       ErrorMessage = "Please enter correct email address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Business Name is required")]
        [RegularExpression("^[A-Z][a-z]*$",
        ErrorMessage = "Please enter valid business name")]
        public string BusinessName { get; set; }
      

        [MaxLength(10)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        public string ModeOfBusiness { get; set; }
        [Required]
        public string Category { get; set; }

       
    }
}
