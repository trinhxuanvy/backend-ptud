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
    public class LocationController : ControllerBase
    {
        private readonly LocationService locationService;

        public LocationController(LocationService _locationService)
        {
            locationService = _locationService;
        }

        [HttpGet("cuahang")]
        public async Task<IActionResult> GetAllViTriCuaHang()
        {
            var lct = await locationService.GetAllViTriCuaHang();
            
            if (lct == null)
            {
                return NotFound();
            }

            return Ok(lct);
        }

        [HttpGet("khachhang")]
        public async Task<IActionResult> GetAllViTriKhachHang()
        {
            var lct = await locationService.GetAllViTriKhachHang();

            if (lct == null)
            {
                return NotFound();
            }

            return Ok(lct);
        }

        [HttpGet("shipper")]
        public async Task<IActionResult> GetAllViTriShipper()
        {
            var lct = await locationService.GetAllViTriShipper();

            if (lct == null)
            {
                return NotFound();
            }

            return Ok(lct);
        }

        [HttpGet("cuahang/{id}")]
        public async Task<IActionResult> GetMotViTriCuaHang(string id)
        {
            var lct = await locationService.GetMotViTriCuaHang(id);

            if (lct == null)
            {
                return NotFound();
            }

            return Ok(lct);
        }
    }
}
