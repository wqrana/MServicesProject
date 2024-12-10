using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSProductAPI.DAL;
using MSProductAPI.Model;

namespace MSProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRespository<Product> productRepository;

        public ProductController(IGenericRespository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpPost]
        public IActionResult PostProduct(Product model)
        {
            string badRequestMsg = "";
            try
            {
               var entity = productRepository.Add(model);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
           return BadRequest(badRequestMsg);
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            string badRequestMsg = "";
            try
            {
                var products = productRepository.GetAll();
                return Ok(products);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpGet, Route("{id}")]
        public IActionResult Get(int id)
        {
            string badRequestMsg = "";
            try
            {
                var product = productRepository.GetById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpPut]
        public IActionResult Put(Product product)
        {
            string badRequestMsg = "";
            try
            {
                var result = productRepository.Update(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            string badRequestMsg = "";
            try
            {
                var result = productRepository.Delete(id);
                if (result > 0)
                    return Ok();
                else
                    badRequestMsg="Error while deleting record";
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
    }
}
