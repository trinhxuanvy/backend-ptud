using DAPTUD.Models;
using DAPTUD.Services;
using Microsoft.AspNetCore.Http;
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
        private readonly SanPhamService productService;

        public SanPhamController(SanPhamService _productService)
        {
            productService = _productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SanPham>> GetProductById(string id)
        {
            var product = await productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpGet]
        public async Task<List<SanPham>> GetAllProducts()
        {
            return await productService.GetAllProduct();
        }
        [HttpGet("essential")]
        public async Task<List<SanPham>> GetEssentialProducts()
        {
            return await productService.EssentialProducts();
        }
        [HttpPost]
        [Route("uploadexcel/{id}")]
        public async Task<List<SanPham>> ImportProductsByExcel(IFormFile file, string id)
        {
            var file1 = Request.Form.Files[0];
            return await productService.ImportProductByExcel(file1,id);
        }
        [HttpPost]
        public async Task<SanPham> InsertNewProduct(SanPham prod)
        {
            return await productService.CreateProduct(prod);
        }
        [HttpGet("find/{name}")]
        public List<SanPham> SearchProducts(string name)
        {
            return productService.SearchProductByName(name);
        }
<<<<<<< HEAD
        [HttpGet("store/{id}")]
        public async Task<List<SanPham>> GetProductByIDStore(string id)
        {
            return await productService.GetProductByIDStore(id);
=======
        [HttpGet("store/{idS}")]
        public async Task<List<SanPham>> GetProductsByStoreID(string idS)
        {
            return await productService.GetProductsByStoreID(idS);
>>>>>>> 588085d9274d8585f1944538bf3d35c8b0cd9f2d
        }
    }
}
