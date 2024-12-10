using Newtonsoft.Json;
using POSWebClient.Models;
using System.Net.Http;

namespace POSWebClient.APIService.Auth
{
    public class AuthenticateService : IAuthenticateService
    {
        private HttpClient httpClient = null!;

        public AuthenticateService(HttpClient httpClient)
        {
            this.httpClient = httpClient;

        }
        public UserSessionInfo Login(UserLoginModel model)
        {
            var response = httpClient.PostAsJsonAsync("AuthenticateAPI", model).Result;
            //httpClient.PostAsync("Account", model).Result;
            //string jsonData = JsonConvert.SerializeObject(model);
            //var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            //var response = httpClient.PostAsync("Account", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string res = response.Content.ReadAsStringAsync().Result;
                var userSessionInfo = JsonConvert.DeserializeObject<UserSessionInfo>(res);
                if (userSessionInfo != null)
                {
                    return userSessionInfo;
                }

            }
            else
            {
                var result = response.Content.ReadAsStringAsync().Result;               
                throw new Exception(result);
            }
            return null;
        }

        public bool Register(UserRegistrationModel model)
        {
           var response = httpClient.PostAsJsonAsync("AuthenticateAPI/1", model).Result;

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorMsg=response.Content.ReadAsStringAsync().Result;
                throw new Exception(errorMsg);
            }
            return false;
        }

     
        bool IAuthenticateService.IsUserAuthenticated(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = httpClient.GetAsync("Account").Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error:{response.StatusCode.ToString()}");
            }
            return true;
        }
    }

        
    }
