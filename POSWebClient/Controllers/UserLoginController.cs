using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSWebClient.APIGateWay;
using POSWebClient.APIService.Auth;
using POSWebClient.Extensions;
using POSWebClient.Models;

namespace POSWebClient.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly IAuthenticateService authenicateService;
        private IAPIGateWayFactory apiGateWayFactory;
        public UserLoginController(IAPIGateWayFactory apiGateWayFactory)
        {
            authenicateService = apiGateWayFactory.AuthenicateService;
            this.apiGateWayFactory = apiGateWayFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Login(UserLoginModel model) {
            if (ModelState.IsValid)
            {
                try
                {
                    UserSessionInfo modelResult = authenicateService.Login(model);
                    if (modelResult != null)
                    {
                        apiGateWayFactory.setJWTSessionToken(modelResult.JwtToken);
                        HttpContext.Session.SetObject("UserSessionInfo", modelResult);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] ="Error While Login user!";
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;                   
                }              
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserRegistrationModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var statusCode = authenicateService.Register(model);
                    if (statusCode == true)
                    {
                        TempData["SuccessMessage"] = "User is Registered Successfully!";
                        return RedirectToAction("Login", "UserLogin");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error While Registering user!";

                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;                   
                }
               
            }
            return View(model);
            
        }
        public IActionResult Logout()
        {
            TempData["SuccessMessage"] = "Logout Successfully";
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserSessionInfo");
           return RedirectToAction("Login", "UserLogin");
        }
    }
}
