using DAPTUD.Models;
using DAPTUD.IDbConfig;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;

namespace DAPTUD.Services
{
    public class SanPhamService
    {
        private readonly IMongoCollection<SanPham> product;

        public SanPhamService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            product = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
        }

        public async Task<List<SanPham>> GetProductById(string id)
        {
            return await product.Find<SanPham>(s => s.id == id).ToListAsync();
        }
        /// <summary>
        /// get all products
        /// </summary>
        /// <returns></returns>
        public async Task<List<SanPham>> GetAllProduct()
        {
            return await product.Find<SanPham>(p => true).ToListAsync();
        }
        /// <summary>
        /// search products by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<SanPham> SearchProductByName(string name)
        {
            return product.AsQueryable<SanPham>().AsEnumerable().Where(p => name.ToLower().All(key => p.tenSanPham.ToLower().Contains(key))).ToList();
        }
        /// <summary>
        /// List essential products
        /// </summary>
        /// <returns></returns>
        public Task<List<SanPham>> EssentialProducts()
        {
            return product.Find(p => p.thietYeu == true).ToListAsync();
        }
        /// <summary>
        /// upload products by excel
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<List<SanPham>> ImportProductByExcel(IFormFile file,string cuahang)
        {
            try
            {
                var listProduct = new List<SanPham>();
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;
                        for (var row = 2; row <= rowcount; row++)
                        {
                            SanPham prod = new SanPham
                            {
                                tenSanPham = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                xuatXu = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                giaTien = Int32.Parse(worksheet.Cells[row, 3].Value.ToString().Trim()),
                                hanSuDung = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()),
                                hinhAnh = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                tenCuaHang = worksheet.Cells[row, 6].Value.ToString().Trim(),
                                tenLoaiHang = worksheet.Cells[row, 7].Value.ToString().Trim(),
                                donViTinh = worksheet.Cells[row, 8].Value.ToString().Trim(),
                                cuaHang = cuahang
                            };
                            listProduct.Add(prod);
                        }
                    }
                }
                foreach (var item in listProduct)
                {
                    await product.InsertOneAsync(item).ConfigureAwait(false);
                }
                return listProduct;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// insert new product
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        public async Task<SanPham> CreateProduct(SanPham prod)
        {
            await product.InsertOneAsync(prod).ConfigureAwait(false);
            return prod;
        }
        public async Task<List<SanPham>> GetProductByIDStore(string id)
        {
            return await product.Find(p => p.cuaHang == id).ToListAsync();
        }

    }

}
