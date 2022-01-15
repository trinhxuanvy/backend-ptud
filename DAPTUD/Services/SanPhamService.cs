using DAPTUD.Models;
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
    }

}
