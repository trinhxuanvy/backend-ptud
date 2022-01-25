using DAPTUD.Models;
using DAPTUD.Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet, Authorize(Roles = "Admin")]
        public Task<List<NguoiDung>> GetAll()
        {
            return cusService.GetAll();
        }

        // GET api/<NguoiDungController>/5
        [HttpGet("cart/{id}")]
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
        [HttpPut("XoaGioHang/{id}")]
        public async Task<IActionResult> ClearCart(string id, DonHang inv)
        {
            var result = await cusService.ClearCart(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // DELETE api/<NguoiDungController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet("cart/{cusid}/{proid}")]
        public async Task<ActionResult> InsertProductToCart(string cusid, string proid)
        {
            var res= await cusService.InsertProductToCart(proid, cusid);
            if (res == null)
            {
                return NotFound("2");
            }
            return Ok(res);
        }
        [HttpGet("cart/{cusid}/{proid}/{num}")]
        public async Task<bool> UpdateNumProductInCart(string cusid, string proid, int num)
        {
            return await cusService.UpdateNumProductInCart(cusid, proid, num);
        }
		[HttpGet("deleteitemcart/{cusid}/{proid}")]
		public NguoiDung DeleteProductInCart(string cusid, string proid)
		{
			return cusService.DeleteProductInCart(cusid, proid);
		}
	}
}
