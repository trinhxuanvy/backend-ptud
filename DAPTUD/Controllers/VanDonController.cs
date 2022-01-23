using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Microsoft.Extensions.Options;
using DAPTUD.Services;
using DAPTUD.Models;

namespace DAPTUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VanDonController : ControllerBase
    {
        private readonly VanDonService vanDonService;

        public VanDonController(VanDonService _vanDonService)
        {
            vanDonService = _vanDonService;
        }
        [HttpGet]
        public Task<List<VanDon>> GetAll()
        {
            return vanDonService.GetAll();
        }

        [HttpGet("invoice/{id}")]
        public async Task<VanDon> GetById(string id)
        {
            return await vanDonService.GetById(id);
        }

        [HttpGet("store/{id}")]

        public async Task<VanDon> GetByIdStore(string id)
        {
            return await vanDonService.GetByIdStore(id);
        }
        [HttpPost]
        public async Task<VanDon> CreateAsync(VanDon VanDon)
        {
            return await vanDonService.CreateAsync(VanDon);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, VanDon vanDonIn)
        {
            var vanDon = await vanDonService.Update(id, vanDonIn);
            if (vanDon == null)
            {
                return NotFound();
            }
            return Ok(vanDon);
        }
    }
}