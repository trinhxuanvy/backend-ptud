using DAPTUD.IDbConfig;
using DAPTUD.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class VanDonService
    {
        private readonly IMongoCollection<VanDon> vanDons;
        private readonly IMongoCollection<DonHang> donHangs;
        public VanDonService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            vanDons = database.GetCollection<VanDon>(dbConfig.VanDonCollectionName);
            donHangs = database.GetCollection<DonHang>(dbConfig.DonHangCollectionName);
        }

        public Task<List<VanDon>> GetAll()
        {
            return vanDons.Find(c => true).ToListAsync();
        }

        public async Task<DonHang> GetById(string id)
        {
            return await donHangs.Find(c => c.id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        public async Task<VanDon> CreateAsync(VanDon VanDon)
        {
            await vanDons.InsertOneAsync(VanDon).ConfigureAwait(false);
            return VanDon;
        }
        public async Task<VanDon> Update(string id, VanDon vanDonIn)
        {
            var vanDon = await vanDons.Find(c => c._id.ToString() == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (vanDon == null)
            {
                return null;
            }
            vanDonIn._id = vanDon._id;
            if (vanDonIn.thoiGianDat == null) vanDonIn.thoiGianDat = vanDon.thoiGianDat;
            if (vanDonIn.trangThai == null) vanDonIn.trangThai = vanDon.trangThai;
            await vanDons.ReplaceOneAsync(c => c._id.ToString() == id, vanDonIn).ConfigureAwait(false);
            return vanDonIn;
        }
    }
}
