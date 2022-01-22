using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class CuaHangService
    {
        private readonly IMongoCollection<CuaHang> stores;

        public CuaHangService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            stores = database.GetCollection<CuaHang>(dbConfig.CuaHangCollectionName);
        }

        public async Task<List<CuaHang>> GetAllCuaHang()
        {
            return await stores.Find(s => true).ToListAsync();
        }

        public async Task<CuaHang> GetCuaHangById (string id)
        {
          return await stores.Find<CuaHang>(s => s.id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }
        
        public async Task<CuaHang> GetCuaHangByOwner(string id)
        {
          return await stores.Find<CuaHang>(s => s.chuCuaHang == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<CuaHang> UpdateCuaHangById (CuaHang storeInput)
        {
            var store = await stores.ReplaceOneAsync(s => s.id == storeInput.id, storeInput).ConfigureAwait(false);
            if (store != null)
            {
                return storeInput;
            }
            return storeInput;
        }
    }
}
