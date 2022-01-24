﻿using DAPTUD.IDbConfig;
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
        private readonly IMongoCollection<CuaHang> store;
        public NguoiDungService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            cus = database.GetCollection<NguoiDung>(dbConfig.NguoiDungCollectionName);
            prod = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
            store = database.GetCollection<CuaHang>(dbConfig.CuaHangCollectionName);
        }
        public async Task<List<ProductCustom>> GetCartById(string id)
        {
            NguoiDung customer = await cus.Find<NguoiDung>(s => s.id == id).FirstOrDefaultAsync();
            List<ProductCustom> cart = new List<ProductCustom>();

            for (int i = 0; i < customer.gioHang.Length; i++)
            {
                ProductCustom tmp = new ProductCustom();
                SanPham product = await prod.Find<SanPham>(s => s.id == customer.gioHang[i].sanPham).FirstOrDefaultAsync();
                CuaHang _store = await store.Find<CuaHang>(s => s.id == product.cuaHang).FirstOrDefaultAsync();
                tmp.productImage = product.hinhAnh;
                tmp.price = product.giaTien;
                tmp.numOfElement = customer.gioHang[i].soLuong;
                tmp.unit = product.donViTinh;
                tmp.total = tmp.price * tmp.numOfElement;
                tmp.product = product.tenSanPham;
                tmp.store = _store.id;
                tmp.productid = product.id;
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
        public async Task<UpdateResult> InsertProductToCart(string prodID, string cusID)
        {
            SanPham product = await prod.Find<SanPham>(p => p.id == prodID).FirstOrDefaultAsync();
            NguoiDung user = await cus.Find<NguoiDung>(u => u.id == cusID).FirstOrDefaultAsync();
            foreach (var item in user.gioHang)
                if (item.sanPham == product.id)
                    return null;
            Cart tmp = new Cart();
            tmp.sanPham = prodID;
            tmp.tenSanPham = product.tenSanPham;
            tmp.soLuong = 1;
            tmp.tongTien = product.giaTien;
            return await cus.UpdateOneAsync(Builders<NguoiDung>.Filter.Eq("id", cusID), Builders<NguoiDung>.Update.Push("gioHang", tmp));
        }
        public async Task<bool> UpdateNumProductInCart(string cusID, string prodID, int num)
        {
            if (num < 1)
                return false;
            NguoiDung user = await cus.Find<NguoiDung>(u => u.id == cusID).FirstOrDefaultAsync();
            SanPham product = await prod.Find<SanPham>(p => p.id == prodID).FirstOrDefaultAsync();
            var update = Builders<NguoiDung>.Update.Combine(
                Builders<NguoiDung>.Update.Set(x=>x.gioHang[-1].soLuong,num),
                Builders<NguoiDung>.Update.Set(x=>x.gioHang[-1].tongTien,num*product.giaTien));
            var filter = Builders<NguoiDung>.Filter.And(
                Builders<NguoiDung>.Filter.Eq(x=>x.id,cusID),
                Builders<NguoiDung>.Filter.ElemMatch(x=>x.gioHang,x=>x.sanPham==prodID)
                );
            var result = await cus.UpdateOneAsync(filter, update);
            return result.IsAcknowledged
                    && result.ModifiedCount > 0;
        }
    }
}
