using Event.Data;
using Event.Models;
using Event.Models.Entity;
using Event.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace Event_Vendor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;
        public UserController(ApplicationContext context)
        {
            _dbContext = context;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userObj)
        {
            if (userObj == null)

                return BadRequest();
            var login = await _dbContext.UserRegistration.FirstOrDefaultAsync(x => x.Email == userObj.Email && x.Password == userObj.Password);
            if (login == null)
                return NotFound(new { Message = "User Not Found" });
            return Ok(new { Message = "Login Success!" });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegister userObj)
        {
            if (userObj == null)

                return BadRequest();
            bool UserExist = _dbContext.UserRegistration.Any(x => x.Email == userObj.Email);


            if (UserExist)
            {

                return Ok(new
                {
                    Message = "Email already exists!"
                });



            }
            await _dbContext.UserRegistration.AddAsync(userObj);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered!"
            }) ;

        }
        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            if (forgotPassword == null)

                return BadRequest();
            var user = await _dbContext.UserRegistration.FirstOrDefaultAsync(x => x.Email == forgotPassword.Email);
            if (user != null)
            {
                var token = GenerateResetToken();
                var resetLink = $"http://localhost:4200/resetpassword?Email={forgotPassword.Email}&token={token}";
                await SendResetEmail(user.Email, resetLink);
               
            }
             return Ok(new
                {
                    Message = "We had sent an email to reset your password!"
                });
           
        }


        private async Task SendResetEmail(string email, string resetLink)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com");

            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("anjujohn206@gmail.com", "vqnl eroo cyak gdqc");
            var message = new MailMessage
            {
                From = new MailAddress("anjujohn206@gmail.com"),
                Subject = "Password Reset",
                Body = $"Click the following link to reset your password: {resetLink}"
            };
            message.To.Add(email);
            await smtpClient.SendMailAsync(message);
        }

        private string GenerateResetToken()
        {
            var token = Guid.NewGuid().ToString();

            _dbContext.SaveChanges();

            return token;
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            if (model == null)

                return BadRequest();
            var user = await _dbContext.UserRegistration.FirstOrDefaultAsync(a => a.Email == model.Email);
            if (user != null)
            {
                user.Password = model.Password;
                await _dbContext.SaveChangesAsync();
            }
            return Ok(new
            {
                Message = "Password Reset Successfully!"
            });
        }
    }
    
}
   