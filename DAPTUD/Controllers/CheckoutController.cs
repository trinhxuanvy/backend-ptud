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

    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutService cusService;

        public CheckoutController(CheckoutService _cusService)
        {
            cusService = _cusService;
        }
        // GET: api/<CheckoutController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CheckoutController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckoutService>> GetCartById(string id)
        {
            var cart = await cusService.GetCartById(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST api/<CheckoutController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CheckoutController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CheckoutController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
