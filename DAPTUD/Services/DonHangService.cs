using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class DonHangService
    {
        private readonly IMongoCollection<DonHang> donHang;
        public DonHangService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            donHang = database.GetCollection<DonHang>(dbConfig.DonHangCollectionName);
        }
        public async Task<List<DonHang>> GetDonHangById(string id)
        {
            return await donHang.Find<DonHang>(s => s.id == id).ToListAsync();
        }
        public async Task<List<DonHang>> GetAllDonHang()
        {
            return await donHang.Find<DonHang>(s => true).ToListAsync();
        }
    }
}
