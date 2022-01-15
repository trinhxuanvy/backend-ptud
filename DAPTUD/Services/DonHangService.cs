using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class InvoiceDetail
    {
        public string product { get; set; }
        public int price { get; set; }
        public int numOfElement { get; set; }
        public string unit { get; set; }
    }
    public class Invoice
    {
        public int ID { get; set; }
        public string invoiceID { get; set; }
        public string timeOrder { get; set; }
        public List<InvoiceDetail> invoiceDetail { get; set; }
        public int total { get; set; }
        public string status { get; set; }
        public bool action { get; set; }
    }
    public class DonHangService
    {
        private readonly IMongoCollection<DonHang> invoices;
        private readonly IMongoCollection<ChiTietDonHang> invoiceDetails;
        private readonly IMongoCollection<SanPham> product;
        public DonHangService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            invoices = database.GetCollection<DonHang>(dbConfig.DonHangCollectionName);
            product = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
            invoiceDetails = database.GetCollection<ChiTietDonHang>(dbConfig.ChiTietDonHangCollectionName);
        }
        public async Task<List<DonHang>> GetInvoiceById(string id)
        {
            return await invoices.Find<DonHang>(s => s.id == id).ToListAsync();
        }
        public async Task<List<Invoice>> GetInfOfInvoicesByCus(string idCus)
        {
            List<Invoice> result = new List<Invoice>();

            List<DonHang> invs = await invoices.Find<DonHang>(s => s.nguoiMua == idCus).ToListAsync();

            int i = 1;
            foreach (DonHang inv in invs)
            {
                List<ChiTietDonHang> invdetails = await invoiceDetails.Find<ChiTietDonHang>(s => s.donHang == inv.id).ToListAsync();

                List<InvoiceDetail> listinvoiceDetails = new List<InvoiceDetail>();

                int tmptotal = 0;

                foreach (ChiTietDonHang invdetail in invdetails)
                {
                    List<SanPham> prods = await product.Find<SanPham>(s => s.id == invdetail.sanPham).ToListAsync();

                    foreach (SanPham product in prods)
                    {
                        InvoiceDetail tmpInvoiceDetail = new InvoiceDetail();
                        tmpInvoiceDetail.product = product.tenSanPham;
                        tmpInvoiceDetail.price = product.giaTien;
                        tmpInvoiceDetail.numOfElement = invdetail.soLuong;
                        tmpInvoiceDetail.unit = product.donViTinh;
                        tmptotal += product.giaTien * invdetail.soLuong;
                        listinvoiceDetails.Add(tmpInvoiceDetail);
                    }
                }

                Invoice tmp = new Invoice();
                tmp.ID = i;
                tmp.invoiceID = inv.id;
                tmp.timeOrder = inv.thoiGianDat.GetDateTimeFormats('d')[0];
                tmp.invoiceDetail = listinvoiceDetails;
                tmp.total = tmptotal;
                tmp.status = inv.tinhTrang;
                tmp.action = inv.tinhTrang == "Đóng gói" ? true : false;
                result.Add(tmp);

                i++;
            }

            return result;
        }
        public async Task<UpdateResult> CancelInvoice(string id)
        {
            var filter = Builders<DonHang>.Filter.Eq("id", id);
            var acceptUpdate = Builders<DonHang>.Update.Set("tinhTrang", "Đã huỷ");
            var denyUpdate = Builders<DonHang>.Update.Set("id", id);

            List<DonHang> invs = await invoices.Find(s => s.id == id).ToListAsync();

            if(invs.Count() > 1)
            {
                return await invoices.UpdateOneAsync(filter, denyUpdate);
            }
            
            foreach (DonHang inv in invs)
            {
                if(inv.tinhTrang != "Đóng gói")
                {
                    return await invoices.UpdateOneAsync(filter, denyUpdate);
                }
            }

            return await invoices.UpdateOneAsync(filter, acceptUpdate);
        }

        public async Task<List<DonHang>> GetAllDonHangForStatistic(string stordId)
        {
            return await donHang.Find<DonHang>(d => d.cuaHang == stordId && d.tinhTrang == "Giao thành công").ToListAsync();
        }
    }
}
