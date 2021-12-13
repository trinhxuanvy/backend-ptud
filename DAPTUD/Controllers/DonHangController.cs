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
        [HttpGet("nguoimua/{id}")]
        public async Task<ActionResult<DonHang>> GetAllDonHangByNguoiMua(string id)
        {
            var donHang = await donHangService.GetAllDonHangByNguoiMua(id);
            if (donHang == null)
            {
                return NotFound();
            }
            return Ok(donHang);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DonHang>> GetDonHangById(string id)
        {
            var donHang = await donHangService.GetDonHangById(id);
            if (donHang == null)
            {
                return NotFound();
            }
            return Ok(donHang);
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
