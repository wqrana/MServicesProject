using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSCustomerAPI.DAL;
using MSCustomerAPI.Model;

namespace MSCustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IGenericRespository<Customer> customerRepo;
     

        public CustomerController(IGenericRespository<Customer> customerRepo)
        {
            this.customerRepo = customerRepo;
           
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = customerRepo.GetAll();
            return Ok(customers);
        }
        [HttpGet, Route("{id}")]
        public IActionResult GetById(int id)
        {
           var customerEntity = customerRepo.GetById(id);
            return Ok(customerEntity);
        }
        [HttpPost] 
        public IActionResult Post(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var result = customerRepo.Add(customer);
              
                return Ok(result);
            }
                //ModelState.IsValid
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var result = customerRepo.Update(customer);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete, Route("{id}")]
        public IActionResult Delete(int id)
        {
            var result = customerRepo.Delete(id);
            if (result > 0)
                return Ok();
            else
                return BadRequest();
        }
    }
}
