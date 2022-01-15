using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class ChiTietDonHangService
    {
        private readonly IMongoCollection<DonHang> invoices;
        private readonly IMongoCollection<ChiTietDonHang> invoiceDetails;
        private readonly IMongoCollection<SanPham> product;
        public ChiTietDonHangService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            invoices = database.GetCollection<DonHang>(dbConfig.DonHangCollectionName);
            product = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
            invoiceDetails = database.GetCollection<ChiTietDonHang>(dbConfig.ChiTietDonHangCollectionName);
        }

        public async Task<List<ChiTietDonHang>> GetAll()
        {
            return await invoiceDetails.Find(c => true).ToListAsync();
        }

        public async Task<List<ChiTietDonHang>> Get(string donHang)
        {
            return await invoiceDetails.Find(c => c.donHang == donHang).ToListAsync();
        }

        public async Task<ChiTietDonHang> CreateAsync(ChiTietDonHang chiTietDonHang)
        {
            await invoiceDetails.InsertOneAsync(chiTietDonHang).ConfigureAwait(false);
            return chiTietDonHang;
        }
    }
}
