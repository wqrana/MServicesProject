using POSWebClient.Models;

namespace POSWebClient.APIService.Auth
{
    public interface IAuthenticateService
    {

        public UserSessionInfo Login(UserLoginModel model);

        public bool Register(UserRegistrationModel model);

        public bool IsUserAuthenticated(string token);
    }
}
