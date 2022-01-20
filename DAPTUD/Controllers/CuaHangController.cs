using DAPTUD.Models;
using DAPTUD.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuaHangController : ControllerBase
    {
        private readonly CuaHangService storeSerive;

        public CuaHangController(CuaHangService _storeService)
        {
            this.storeSerive = _storeService;
        }    

        [HttpGet]
        public async Task<IActionResult> GetAllCuaHang()
        {
            var stores = await storeSerive.GetAllCuaHang();

            if (stores == null)
            {
                return NotFound();
            }

            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CuaHang>> GetCuaHangById(string id)
        {
            var store = await storeSerive.GetCuaHangById(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        [HttpGet("owner/{id}")]
        public async Task<ActionResult<CuaHang>> GetCuaHangByOwner(string id)
        {
            var store = await storeSerive.GetCuaHangByOwner(id);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }
    }
}
