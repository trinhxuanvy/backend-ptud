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
    public class NguoiDungController : ControllerBase
    {
        // GET: api/<NguoiDungController>
        private readonly NguoiDungService cusService;

        public NguoiDungController(NguoiDungService _cusService)
        {
            cusService = _cusService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<NguoiDungController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ProductCustom>>> GetCartById(string id)
        {
            var cart = await cusService.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST api/<NguoiDungController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NguoiDungController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NguoiDungController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
