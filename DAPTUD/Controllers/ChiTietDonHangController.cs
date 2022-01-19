﻿using System;
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

namespace DiChoThue.Controllers
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
    }
}