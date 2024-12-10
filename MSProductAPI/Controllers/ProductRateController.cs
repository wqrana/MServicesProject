using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSProductAPI.DAL;
using MSProductAPI.Model;

namespace MSProductAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductRateController : ControllerBase
    {
        private IProductRateRespository<ProductRate> productRateRespository;
        private string badRequestMsg = "";
        public ProductRateController( IProductRateRespository<ProductRate> productRateRespository) {
        
             this.productRateRespository = productRateRespository;
        }
        [HttpGet]
        public IActionResult GetAllProductRates()
        {            
            try
            {
                var productRates = productRateRespository.GetAll();
                return Ok(productRates);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpGet]
        public IActionResult GetAllProductEffectiveRates()
        {
            try
            {
                var allProductEffectiveRates = productRateRespository.GetAllEffectiveRate();
                return Ok(allProductEffectiveRates);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }

        [HttpGet, Route("{id}")]
        public IActionResult GetProductRateById(int id)
        {
            try
            {
                var productRate = productRateRespository.GetById(id);
                return Ok(productRate);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpGet, Route("{id}")]
        public IActionResult GetProductEffectiveRateByProduct(int id)
        {
            try
            {
                var productEffectiveRate = productRateRespository.GetEffectiveRateByProductId(id);
                return Ok(productEffectiveRate);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpGet, Route("{id}")]
        public IActionResult GetAllProductRateByProduct(int id)
        {
            try
            {
                var productEffectiveRate = productRateRespository.GetAllRateByProductId(id);
                return Ok(productEffectiveRate);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpPost]  
        public IActionResult PostProductRate(ProductRate model)
        {
            try
            {
                var productRate = productRateRespository.Add(model);
                return Ok(productRate);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpPut]
        public IActionResult PutProductRate(ProductRate model)
        {
            try
            {
                var productRate = productRateRespository.Update(model);
                return Ok(productRate);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpDelete, Route("{id}")]
       // [HttpDelete]
        public IActionResult DeleteProductRate(int id)
        {
            string badRequestMsg = "";
            try
            {
                var result = productRateRespository.Delete(id);
                if (result > 0)
                    return Ok();
                else
                    badRequestMsg = "Error while deleting record";
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
    }
}
