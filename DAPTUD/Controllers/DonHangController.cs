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
        private readonly DonHangService donHangService;

        public DonHangController(DonHangService _donHangService)
        {
            donHangService = _donHangService;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<DonHang>> GetAllDonHang()
        {
            var store = await donHangService.GetAllDonHang();
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHangById(string id)
        {
            var store = await donHangService.GetDonHangById(id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
