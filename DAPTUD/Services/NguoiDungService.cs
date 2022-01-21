using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class NguoiDungService
    {
        private readonly IMongoCollection<NguoiDung> cus;
        private readonly IMongoCollection<SanPham> prod;
        public NguoiDungService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            cus = database.GetCollection<NguoiDung>(dbConfig.NguoiDungCollectionName);
            prod = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
        }
        public async Task<List<ProductCustom>> GetCartById(string id)
        {
            NguoiDung customer = await cus.Find<NguoiDung>(s => s.id == id).FirstOrDefaultAsync();
            List<ProductCustom> cart = new List<ProductCustom>();

            for (int i = 0; i < customer.gioHang.Length; i++)
            {
                ProductCustom tmp = new ProductCustom();
                SanPham product = await prod.Find<SanPham>(s => s.id == customer.gioHang[i].sanPham).FirstOrDefaultAsync();
                
                tmp.price = product.giaTien;
                tmp.numOfElement = customer.gioHang[i].soLuong;
                tmp.unit = product.donViTinh;
                tmp.total = tmp.price * tmp.numOfElement;
                tmp.product = product.tenSanPham + "(" + product.donViTinh + ")";
                cart.Add(tmp);
            }

            return cart;
        }

        public async Task<UpdateResult> ClearCart(string id)
        {
            var filter = Builders<NguoiDung>.Filter.Eq("id", id);
            var acceptDefault = Builders<NguoiDung>.Update.Set("id", id);
            var acceptUpdate = Builders<NguoiDung>.Update.PopLast("gioHang");

            NguoiDung customer = await cus.Find<NguoiDung>(s => s.id == id).FirstOrDefaultAsync();
            for (int i = 0; i < customer.gioHang.Length; i++)
            {
                await cus.UpdateOneAsync(filter, acceptUpdate);
            }

            return await cus.UpdateOneAsync(filter, acceptDefault);
        }

        public async Task<NguoiDung> GetUserByEmail(string email)
        {
            return await cus.Find<NguoiDung>(s => s.email == email).FirstOrDefaultAsync();
        }

        public async Task<NguoiDung> GetUserByEmailAndPassword(string email, string matkhau)
        {
            return await cus.Find<NguoiDung>(s =>  s.email == email && s.matKhau == matkhau ).FirstOrDefaultAsync();
        }

        public async Task<NguoiDung> CreateAsync(NguoiDung user)
        {
            await cus.InsertOneAsync(user).ConfigureAwait(false);
            return user;
        }

        public Task<List<NguoiDung>> GetAll()
        {
            return cus.Find(c => true).ToListAsync();
        }
    }
}
