using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using POSWebClient.APIGateWay;
using POSWebClient.APIService.Operional;
using POSWebClient.Extensions;
using POSWebClient.Models;

namespace POSWebClient.Controllers
{
    [AuthorizeUser]
    public class ProductRateController : BaseApplicationController
    {
        private readonly IAPIProductRateService<ProductRateModel> productRateService;
        private readonly IAPIGenericService<ProductModel> productService;

        public ProductRateController(IAPIGateWayFactory apiGateWayFactory)
        {
            productService = apiGateWayFactory.ProductService;
            productRateService =apiGateWayFactory.ProductRateService;
        }
        public IActionResult Index()
        {      
            try
            {
                var model = productRateService.GetAllEffectiveProductRate().OrderBy(o => o.Product.Code);
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void setProductDDLData()
        {
            ViewBag.ProductList = productService.GetAll().OrderBy(o => o.Code).Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Code + " - " + s.Name });
        }
        public IActionResult CreateOrEdit(int id)
        {
            //ViewBag.ProductList = productService.GetAll().OrderBy(o => o.Code).Select(s => new SelectListItem() { Value = s.Id.ToString(), Text = s.Code + " - " + s.Name });
            setProductDDLData();
            ProductRateModel model = null;
            if (id > 0)
            {
                model = productRateService.Get(id);
            }
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult CreateOrEdit(ProductRateModel model)
        {
            int statusCode = 1;
            string message = "Product Rate is successfully Added/Updated";
            ViewBag.AhocPopupTitle = "Create/Edit Product Rate";

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Id > 0)
                    {
                        productRateService.Update(model);
                    }
                    else
                    {
                        productRateService.Create(model);
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
                setProductDDLData();
                return PartialView(model);

            }

            return Json(new { statusCode = statusCode, message = message });
        }
        public IActionResult Delete(int id)
        {
            ProductRateModel model = null;
            if (id > 0)
            {
                model = productRateService.Get(id);
            }
            return PartialView(model);
        }
        [HttpDelete]
        public IActionResult ConfirmDelete(int id)
        {
            int statusCode = 1;
            string message = "Product Rate is successfully deleted";
            var res = productRateService.Delete(id);
            if (res == null)
            {
                statusCode = -1;
                message = "Error deleting record";
            }

            return Json(new { statusCode = statusCode, message = message });
        }
        public IActionResult Details(int id)
        {
            ProductRateModel model = null;
            if (id > 0)
            {
                model = productRateService.Get(id);
            }
            return PartialView(model);
        }
    }
}
