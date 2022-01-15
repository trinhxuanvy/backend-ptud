using DAPTUD.Models;
using DAPTUD.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DAPTUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly DonHangService invoiceService;

        public DonHangController(DonHangService _donHangService)
        {
            invoiceService = _donHangService;
        }

        // GET: api/<ValuesController>
        [HttpGet("nguoimua/{id}")]
        public async Task<ActionResult<DonHang>> GetInfOfInvoicesByCus(string id)
        {
            var invoices = await invoiceService.GetInfOfInvoicesByCus(id);
            if (invoices == null)
            {
                return NotFound();
            }
            return Ok(invoices);
        }
        
        
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetInvoiceById(string id)
        {
            var invoice = await invoiceService.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        /*[HttpPut("{id}")]
        public async Task<IActionResult> CancelInvoice(string id)
        {
            var result = await invoiceService.CancelInvoice(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }*/
        [HttpPut("{id}")]
        public async Task<IActionResult> CancelInvoice(string id, DonHang inv)
        {
            var result = await invoiceService.CancelInvoice(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
