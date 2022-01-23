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

        public async Task<Shipper> GetUserByEmail(string email)
        {
            return await shippers.Find<Shipper>(s => s.email == email).FirstOrDefaultAsync();
        }

        public async Task<Shipper> GetUserByEmailAndPassword(string email, string matkhau)
        {
            return await shippers.Find<Shipper>(s => s.email == email && s.matKhau == matkhau).FirstOrDefaultAsync();
        }
        public Task<List<Shipper>> GetAll()
        {
            return shippers.Find(c => true).ToListAsync();
        }

        public async Task<Shipper> CreateAsync(Shipper user)
        {
            await shippers.InsertOneAsync(user).ConfigureAwait(false);
            return user;
        }

        public async Task<Shipper> GetById(string id)
        {
            var shipper = await shippers.Find(c => c._id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            return shipper;
        }
    }
}
