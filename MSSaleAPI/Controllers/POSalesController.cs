
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSSalesAPI.DAL;
using MSSalesAPI.Model;

namespace MSSalesAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class POSalesController : ControllerBase
    {
        private IPOSalesRespository<POSales,POSalesDetail> pOSalesRespository;
        private string badRequestMsg = "";
        public POSalesController(IPOSalesRespository<POSales, POSalesDetail> pOSalesRespository)
        {

            this.pOSalesRespository = pOSalesRespository;
        }
        [HttpGet]
        public IActionResult GetAllPOSales()
        {
            try
            {
                var allPosales = pOSalesRespository.GetAll();
                return Ok(allPosales);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpGet]
      

        [HttpGet, Route("{id}")]
        public IActionResult GetPOSalesById(int id)
        {
            try
            {
                var pOSales = pOSalesRespository.GetById(id);
                return Ok(pOSales);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpGet, Route("{id}")]
        public IActionResult GetAllPOSalesDetailsBySalesId(int id)
        {
            try
            {
                var posSalesDetails = pOSalesRespository.GetAllDetailsBySalesId(id);
                return Ok(posSalesDetails);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpGet, Route("{id}")]
        public IActionResult GetPOSalesDetailById(int id)
        {
            try
            {
                var recordSet = pOSalesRespository.GetSalesDetailById(id);
                return Ok(recordSet);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpPost]
        public IActionResult PostPOSales(POSales model)
        {
            try
            {
                var entity = pOSalesRespository.Add(model);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpPost]
        public IActionResult PostPOSalesDetail(POSalesDetail model)
        {
            try
            {
                var entity = pOSalesRespository.AddSalesDetail(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpPut]
        public IActionResult PutPOSales(POSales model)
        {
            try
            {
                var result = pOSalesRespository.Update(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }
        [HttpPut]
        public IActionResult PutPOSalesDetail(POSalesDetail model)
        {
            try
            {
                var result = pOSalesRespository.UpdateSalesDetail(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                badRequestMsg = ex.Message;
            }
            return BadRequest(badRequestMsg);
        }

        [HttpDelete, Route("{id}")]
        public IActionResult DeletePOSales(int id)
        {
            string badRequestMsg = "";
            try
            {
                var result = pOSalesRespository.Delete(id);
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
        [HttpDelete, Route("{id}")]
        public IActionResult DeletePOSalesDetail(int id)
        {
            string badRequestMsg = "";
            try
            {
                var result = pOSalesRespository.DeleteSalesDetail(id);
                if (result > 0)
                    return Ok(result);
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
