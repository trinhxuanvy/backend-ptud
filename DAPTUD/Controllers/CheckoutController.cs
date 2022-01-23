using DAPTUD.Models;
using DAPTUD.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
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
        
        [HttpPost("online")]
        public IActionResult Create()
        {
            var domain = "http://localhost:4200";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Price = "price_1KJes3HfeS4VmIfg0D1tKcYe",
                    Quantity = 1,
                  }
                },
                Mode = "payment",
                /*SuccessUrl = domain + "/invoice?success=true",
                CancelUrl = domain + "/invoice?success=false",*/
                SuccessUrl = domain + "/payment/success",
                CancelUrl = domain + "/payment/failed",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            // Response.Headers.Add("Location", session.Url);
            // return new StatusCodeResult(303);
            return Ok(session.Url.ToString());
        }
    }
}
