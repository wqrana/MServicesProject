using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SharedMessageBroker;
using SharedMessageBroker.Model;

namespace MSCustomerAPI.Helper
{
    public class CustomLog : ActionFilterAttribute //, IActionFilter
    {
        private IMessageSender messageSender;
        
        public CustomLog()
        {
           
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        { 
          var messageSender= this.messageSender = context.HttpContext.RequestServices.GetService(typeof(IMessageSender))
                as IMessageSender;

            var logType = "Info";
            var message = "Action is Executed successfully";
            if (context.Exception != null)
            {
                logType = "Error";
                message = context.Exception.Message;
            }
            else if (context.HttpContext.Response.StatusCode != 200)
            {
                logType = "Error";
                message = $"Action Executing Failed, StatusCode: {context.HttpContext.Response.StatusCode}";
            }
            var appLog = new LoggingModel
            {
                LoggingClass = context.Controller.GetType().Name,
                LoggingMethod = context.ActionDescriptor.RouteValues["action"],
                LoggingProgram = context.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().ApplicationName,
                Message = message,
                LogType = logType

            };
            messageSender.InitSendingExchange(QueueType.Logger);
            messageSender.SendMessage<LoggingModel>(appLog, QueueType.Logger);
            messageSender.CloseSender();
        }
   
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var appLog = new LoggingModel
            {
                LoggingClass = context.Controller.GetType().Name,
                LoggingMethod = context.ActionDescriptor.DisplayName,
                LoggingProgram = "MSAuthenticationAPI",
                Message = "Action is Executing",
                LogType = "Info"
            };
            
            //messageSender.SendMessage<LoggingModel>(appLog);
        }
    }
}
