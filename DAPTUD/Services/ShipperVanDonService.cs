using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class ShipperVanDonService
    {
        private readonly IMongoCollection<ShipperVanDon> shipper_vandon;

        public ShipperVanDonService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            shipper_vandon = database.GetCollection<ShipperVanDon>(dbConfig.ShipperVanDonCollectionName);
        }


        public async Task<List<ShipperVanDon>> GetDonHangById(string id)
        {
            return await shipper_vandon.Find(s => s.vandonid == id).ToListAsync();
        }

    }
}