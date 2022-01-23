using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class ShipperService
    {
        private readonly IMongoCollection<Shipper> shippers;
        public ShipperService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            shippers = database.GetCollection<Shipper>(dbConfig.ShipperCollectionName);
        }
        public Task<List<Shipper>> GetAll()
        {
            return shippers.Find(c => true).ToListAsync();
        }

        public async Task<Shipper> GetById(string id)
        {
            var shipper = await shippers.Find(c => c._id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            return shipper;
        }

        public async Task<Shipper> UpdateShipperById(Shipper shipperInput)
        {
            var shipper = await shippers.ReplaceOneAsync(s => s._id == shipperInput._id, shipperInput).ConfigureAwait(false);
            if (shipper != null)
            {
                return shipperInput;
            }
            return shipperInput;
        }
        public async Task<Shipper> UpdateShipperStatusById(int status, string id)
        {
            Shipper shipper = await shippers.Find(c => c._id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            shipper.trangThaiHoatDong = status;
            var updatedShipper = await shippers.ReplaceOneAsync(c => c._id == id, shipper).ConfigureAwait(false);
            return shipper;
        }
    }
}
