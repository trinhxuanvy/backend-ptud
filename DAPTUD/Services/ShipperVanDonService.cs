using DAPTUD.IDbConfig;
using DAPTUD.Models;
using Microsoft.AspNetCore.Mvc;
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
            return await shipper_vandon.Find(s => s.shipper == id).ToListAsync();
        }
        public async Task<ShipperVanDon> GetDonHangOnById(string id)
        {
            var donhangs= await shipper_vandon.Find(s => s.shipper == id).ToListAsync();
            foreach(ShipperVanDon donhang in donhangs)
            {
                if(donhang.trangthai =="Đã nhận")
                {
                    return donhang;
                }
            }
            return donhangs[0];
        }
        public async Task<ShipperVanDon> UpdateShipperVanDonStatusById(string status, string id)
        {
            ShipperVanDon shippervandon = await shipper_vandon.Find(c => c._id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            shippervandon.trangthai = status;
            var updatedShipper = await shipper_vandon.ReplaceOneAsync(c => c._id == id, shippervandon).ConfigureAwait(false);
            return shippervandon;
        }
        public async Task<ShipperVanDon> UpdateShipperVanDonStatusesById(string shipperid, string vandonid)
        {
            List<ShipperVanDon> shipperNotIn = new List<ShipperVanDon>();
            List<ShipperVanDon> shippervandon = await shipper_vandon.Find(c => c.vandonid == vandonid).ToListAsync();
            foreach(ShipperVanDon ShippernotinVanDon in shippervandon)
            {
                if(ShippernotinVanDon.shipper==shipperid)
                {
                    
                } 
                else
                {
                }
            }
            foreach (ShipperVanDon ShippernotinVanDon in shipperNotIn)
            {
                var updatedShipper = await shipper_vandon.ReplaceOneAsync(c => c._id == ShippernotinVanDon._id, ShippernotinVanDon).ConfigureAwait(false);
            }   
            return shippervandon[0];
        }

        public async Task<ShipperVanDon> MakeShipperVanDon(ShipperVanDon data)
        {
            await shipper_vandon.InsertOneAsync(data);
            return data;
        }

    }
}