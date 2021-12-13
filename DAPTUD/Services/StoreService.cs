using DAPTUD.Entities;
using DAPTUD.IDbConfig;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class StoreService
    {
        private readonly IMongoCollection<Store> _stores;

        public StoreService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            _stores = database.GetCollection<Store>(dbConfig.StoresCollectionName);
        }

        public async Task<List<Store>> GetStoreById(string id)
        {
            return await _stores.Find<Store>(s => s.id == id).ToListAsync();
        }
    }

}
