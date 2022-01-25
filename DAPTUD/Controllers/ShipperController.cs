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
    public class ShipperController : ControllerBase
    {
        private readonly ShipperService shipperService;

        public ShipperController(ShipperService _shipperService)
        {
            shipperService = _shipperService;
        }

        [HttpGet]
        public Task<List<Shipper>> GetAll()
        {
            return shipperService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Shipper> GetById(string id)
        {
            return await shipperService.GetById(id);
        }

        [HttpPost("update/{id}")]
        public async Task<Shipper> UpdateShipperStatusById(string id, Shipper status)
        {
            return await shipperService.UpdateShipperStatusById(status.trangThaiHoatDong,id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShipperById(Shipper shipper)
        {
            var newShipper = await shipperService.UpdateShipperById(shipper);

            if (newShipper == null)
            {
                return NotFound();
            }
            return Ok(newShipper);
        }


    }
}