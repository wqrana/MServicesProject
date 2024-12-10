using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using POSWebClient.APIGateWay;
using POSWebClient.APIService.Operional;
using POSWebClient.Extensions;
using POSWebClient.Models;

namespace POSWebClient.Controllers
{
    [AuthorizeUser]
    public class ProductController : Controller
    {
        private readonly IAPIGenericService<ProductModel> productService;

        public ProductController(IAPIGateWayFactory gateWayFactory)
        {
            this.productService = gateWayFactory.ProductService;

        }
        public IActionResult Index()
        {
            try
            {
               var proList= productService.GetAll().OrderBy(o=>o.Id);

                return View(proList);
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        public IActionResult CreateOrEdit(int id)
        {
             ProductModel model = null;
            if (id > 0)
            {
                model = productService.Get(id);
            }
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(ProductModel model)
        {
            int statusCode = 1;
            string message = "Product is successfully Added/Updated";
            ViewBag.AhocPopupTitle = "Create/Edit Product";

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id > 0)
                    {
                        productService.Update(model);
                    }
                    else
                    {
                        productService.Create(model);
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    statusCode = -1;
                }
            }
            else
            {
                return PartialView(model);

            }

            return Json(new { statusCode = statusCode, message = message });
        }
        public IActionResult Delete(int id)
        {
            ProductModel model = null;
            if (id > 0)
            {
                model = productService.Get(id);
            }
            return PartialView(model);
        }
        [HttpDelete]
        public IActionResult ConfirmDelete(int id)
        {
            int statusCode = 1;
            string message = "Product is successfully deleted";
            var res = productService.Delete(id);
            if (res == null)
            {
                statusCode = -1;
                message = "Error deleting record";
            }

            return Json(new { statusCode = statusCode, message = message });
        }
        public IActionResult Details(int id)
        {
            ProductModel model = null;
            if (id > 0)
            {
                model = productService.Get(id);
            }
            return PartialView(model);
        }
    }
}
