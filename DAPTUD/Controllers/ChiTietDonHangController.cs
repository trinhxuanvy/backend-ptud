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
    public class ChiTietDonHangController : ControllerBase
    {
        private readonly ChiTietDonHangService invoiceDetailService;

        public ChiTietDonHangController(ChiTietDonHangService _donHangService)
        {
            invoiceDetailService = _donHangService;
        }
        [HttpGet]
        public Task<List<ChiTietDonHang>> GetAll()
        {
            return invoiceDetailService.GetAll();
        }
        [HttpGet("getHaveName/{donHang}")]
        public Task<Invoice> GetOneHaveNameProduct(string donHang)
        {
            return invoiceDetailService.GetOneHaveNameProduct(donHang);
        }
        [HttpGet("{donHang}")]
        public Task<List<ChiTietDonHang>> Get(string donHang)
        {
            return invoiceDetailService.Get(donHang);
        }

        [HttpPost]
        public async Task<ChiTietDonHang> CreateAsync(ChiTietDonHang chiTietDonHang)
        {
            return await invoiceDetailService.CreateAsync(chiTietDonHang);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var invoiceDetail = await invoiceDetailService.GetById(id);
            if(invoiceDetail == null)
            {
                return NotFound();
            }

            return Ok(invoiceDetail);
        }
    }
}