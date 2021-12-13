using DAPTUD.Entities;
using DAPTUD.IDbConfig;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class SanPhamService
    {
        private readonly IMongoCollection<SanPham> sanPham;

        public SanPhamService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            sanPham = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
        }

        public async Task<List<SanPham>> GetSanPhamById(string id)
        {
            return await sanPham.Find<SanPham>(s => s.id == id).ToListAsync();
        }
    }

}
