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
        //Get all donHangs
        [HttpGet]
        public Task<List<DonHang>> GetAll()
        {
            return invoiceService.GetAll();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<DonHang> CreateAsync(DonHang donHang)
        {
            return await invoiceService.CreateAsync(donHang);
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
        [HttpPut("HuyDonHang/{id}")]
        public async Task<IActionResult> CancelInvoice(string id, DonHang inv)
        {
            var result = await invoiceService.CancelInvoice(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, DonHang donHangIn)
        {
            var donHang = await invoiceService.Update(id, donHangIn);
            if (donHang == null)
            {
                return NotFound();
            }
            return Ok(donHang);
        }
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var donHang = await invoiceService.DeleteAsync(id);
            if (donHang == null)
            {
                return NotFound();
            }
            return Ok(donHang);
        }
    }
}
