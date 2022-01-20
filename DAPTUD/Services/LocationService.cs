using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class LocationService
    {
        private readonly IMongoCollection<ViTriKhachHang> lctKhachHang;
        private readonly IMongoCollection<ViTriCuaHang> lctCuaHang;
        private readonly IMongoCollection<ViTriShipper> lctShipper;

        public LocationService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            lctCuaHang = database.GetCollection<ViTriCuaHang>(dbConfig.ViTriCuaHangCollectionName);
            lctKhachHang = database.GetCollection<ViTriKhachHang>(dbConfig.ViTriKhachHangCollectionName);
            lctShipper = database.GetCollection<ViTriShipper>(dbConfig.ViTriShipperCollectionName);
        }

        public async Task<List<ViTriCuaHang>> GetAllViTriCuaHang()
        {
            return await lctCuaHang.Find(l => true).ToListAsync();
        }

        public async Task<List<ViTriKhachHang>> GetAllViTriKhachHang()
        {
            return await lctKhachHang.Find(l => true).ToListAsync();
        }

        public async Task<List<ViTriShipper>> GetAllViTriShipper()
        {
            return await lctShipper.Find(l => true).ToListAsync();
        }
    }
}
