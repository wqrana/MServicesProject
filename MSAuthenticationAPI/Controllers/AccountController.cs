using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using MSAuthenticationAPI.DAL;
using MSAuthenticationAPI.Helper;
using MSAuthenticationAPI.Model;
using SharedMessageBroker;
using SharedMessageBroker.Model;

namespace MSAuthenticationAPI.Controllers
{
    [CustomLog]
    [Route("api/[controller]")]   
    [ApiController]   
    
    public class AccountController : BaseMSController
    {
        private readonly ApplicationUserDBContext dBContext;
        private readonly JWTSettings jWTSettings;
        public AccountController( ApplicationUserDBContext dBContext,JWTSettings jWTSettings,IMessageSender messageSender):base(messageSender)
        {
            this.dBContext = dBContext;
            this.jWTSettings = jWTSettings;
           
        }

        [HttpPost]
        
        public IActionResult PostUserAthentication(LoginUserModel user)
        {   var errorModel = new ErrorModel() { Code = -1,Message="Error" };
           /// messageSender.SendMessage<LoggingModel>(new LoggingModel { LoggingClass = "AccountController", LoggingMethod = "PostUserAthentication", LoggingProgram="MSAuthenticationAPI", Message = $"User Logging: {user.Email}"});
           
            try
            {
                var appUser = dBContext.ApplicationUsers.Where(x => x.Email == user.Email).FirstOrDefault();
                if (appUser != null)
                {
                    if (!string.IsNullOrEmpty(appUser.Password))
                    {
                        if (user.Password == EncryptUtilityHelper.Decrypt(appUser.Password))
                        {
                            var token = JWTTokenUtilityHelper.GenerateJwtToken(appUser, jWTSettings);
                            var authUser = new AuthenticatedUserModel { Id = appUser.Id, Email = appUser.Email, FirstName = appUser.FirstName, JwtToken = token };
                            return Ok(authUser);
                        }
                    }
                    // errorModel.Message = "Invalid Credentials";
                    return BadRequest("Invalid User/Password.");
                }
                else
                {
                    // errorModel.Message = "Invalid Credentials";
                    return BadRequest("Invalid User/Password.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //errorModel.Message = "Internal Server Error!";
            return BadRequest("Internal Server Error!");
        }
        [HttpPost("{id}")]
        public IActionResult PostUserRegistration(ApplicationUser registerUser, int id)
        {
            if (registerUser != null)

            {
                var existingUser = dBContext.ApplicationUsers.Where(x => x.Email == registerUser.Email).FirstOrDefault();
                if (existingUser == null)
                {
                    registerUser.Password = EncryptUtilityHelper.Encrypt(registerUser.Password);
                    dBContext.ApplicationUsers.Add(registerUser);
                    dBContext.SaveChanges();
                    return Ok(registerUser);
                }
                else
                {
                    return BadRequest("User Already Exists");
                }

            }
            return BadRequest("Invalid register user data");
        }
        [HttpGet]
        [Authorize]
        public IActionResult GetIsAuthorized()
        {
            return Ok("Authorized");
        }
    }
}
