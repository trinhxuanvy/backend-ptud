using DAPTUD.Entities;
using DAPTUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SanPhamController : ControllerBase
    {
        private readonly SanPhamService sanPhamService;

        public SanPhamController(SanPhamService _sanPhamService)
        {
            sanPhamService = _sanPhamService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SanPham>> GetSanPhamById(string id)
        {
            var sanPham = await sanPhamService.GetSanPhamById(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return Ok(sanPham);
        }
    }
}
