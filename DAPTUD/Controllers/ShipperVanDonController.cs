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
    public class ShipperVanDonController : ControllerBase
    {
        private readonly ShipperVanDonService shippervandonService;

        public ShipperVanDonController(ShipperVanDonService _shippervandonService)
        {
            shippervandonService = _shippervandonService;
        }


        [HttpGet("{id}")]
        public async Task<List<ShipperVanDon>> GetDonHangById(string id)
        {
            return await shippervandonService.GetDonHangById(id);
        }
        [HttpGet("vandon/{id}")]
        public async Task<ShipperVanDon> GetDonHangOnById(string id)
        {
            return await shippervandonService.GetDonHangOnById(id);
        }

        [HttpPost]
        public async Task<ActionResult> MakeShipperVanDon(ShipperVanDon data)
        {
            var store = await shippervandonService.MakeShipperVanDon(data);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }

        [HttpPost("{id}")]
        public async Task<ShipperVanDon> updateStatusById(string id,ShipperVanDon status)
        {
            return await shippervandonService.UpdateShipperVanDonStatusById(status.trangthai,id);
        }

        [HttpPost("update/{shipperid}")]
        public async Task<ShipperVanDon> UpdateShipperVanDonStatusesById(string shipperid, ShipperVanDon status)
        {
            return await shippervandonService.UpdateShipperVanDonStatusesById(shipperid, status.vandonid);
        }


    }
}