using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace POSWebClient.Controllers
{
    public class BaseApplicationController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
           var actionName = filterContext.ActionDescriptor.RouteValues["action"];
            if (actionName == "Index")
            {
                ViewBag.controllerName = filterContext.ActionDescriptor.RouteValues["controller"];
            }                   
        }
        protected string GetModelError()
        {
            string errorMessage = "";
            foreach (var modelStateVal in ModelState.Values)
            {
                foreach (ModelError error in modelStateVal.Errors)
                {
                    errorMessage += "| " + error.ErrorMessage;
                }
            }
            return errorMessage;
        }


    }
}
