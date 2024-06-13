namespace Event.Models.Entity
{
    public class ResetPassword
    {
     
        public string Email {  get; set; }
        public string token { get; set; }
        public string Password {  get; set; }
        public string ConfirmPassword { get; set; }

    }
}
