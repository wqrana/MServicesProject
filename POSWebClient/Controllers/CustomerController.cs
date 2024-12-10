using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using POSWebClient.APIGateWay;
using POSWebClient.APIService;
using POSWebClient.APIService.Operional;
using POSWebClient.Extensions;
using POSWebClient.Models;
using System.Reflection;

namespace POSWebClient.Controllers
{
    [AuthorizeUser]
    public class CustomerController : BaseApplicationController
    {
        private IAPIGenericService<CustomerModel> customerService;
        public CustomerController(IAPIGateWayFactory apiGateWayFactory )
        {
            customerService = apiGateWayFactory.CustomerService;
        }
        public IActionResult Index()
        {
            var model=customerService.GetAll().OrderByDescending(o=>o.Id);
            return View(model);
        }
        public IActionResult CreateOrEdit(int id)
        {
            CustomerModel model = null;
            if(id >0) {
                model = customerService.Get(id);
            }
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(CustomerModel model)
        {
            int statusCode = 1;
            string message  = "Customer is successfully Added/Updated";
            ViewBag.AhocPopupTitle = "Create/Edit Customer";

            if(ModelState.IsValid)
            {
                try
                {
                    if (model.Id > 0)
                    {
                        customerService.Update(model);
                    }
                    else
                    {
                        customerService.Create(model);
                    }
                } catch (Exception ex)
                {
                    message = ex.Message;
                    statusCode = -1;
                }               
            }
            else
            {
                return PartialView(model);

            }

            return Json(new {statusCode=statusCode,message=message });
        }
        public IActionResult Delete(int id)
        {
            CustomerModel model = null;
            if (id > 0)
            {
                model = customerService.Get(id);
            }
            return PartialView(model);
        }
        [HttpDelete]
        public IActionResult ConfirmDelete(int id)
        {
            int statusCode = 1;
            string message = "Customer is successfully deleted";
           var res= customerService.Delete(id);
            if(res==null)
            {
                statusCode = -1;
                message = "Error deleting record";
            }

            return Json(new { statusCode = statusCode, message = message });
        }
        public IActionResult Details(int id)
        {
            CustomerModel model = null;
            if (id > 0)
            {
                model = customerService.Get(id);
            }
            return PartialView(model);
        }
    }
}
