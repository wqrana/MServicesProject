using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedMessageBroker;
using SharedMessageBroker.Model;

namespace MSAuthenticationAPI.Controllers
{
    public class BaseMSController : ControllerBase
    {
        protected readonly IMessageSender messageSender;
        public BaseMSController(IMessageSender messageSender) {

            this.messageSender = messageSender;
        }

    }
}
