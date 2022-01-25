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

        public string idInvoiceDetail { get; set; }
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
        public string payment { get; set; }
        public string oldStatus { get; set; }
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
                        tmpInvoiceDetail.idInvoiceDetail = invdetail.id;
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
                tmp.oldStatus = inv.tinhTrangCu;
                tmp.payment = inv.phuongThucThanhToan;
                tmp.action = inv.tinhTrang == "Đóng gói" || inv.tinhTrang == "Mới tạo" ? true : false;
                result.Add(tmp);

                i++;
            }

            return result;
        }

        public async Task<List<Invoice>> GetInfOfInvoicesByStore(string idStore)
        {
            List<Invoice> result = new List<Invoice>();

            List<DonHang> invs = await invoices.Find<DonHang>(s => s.cuaHang == idStore).ToListAsync();

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
                tmp.oldStatus = inv.tinhTrangCu;
                tmp.payment = inv.phuongThucThanhToan;
                tmp.action = (inv.tinhTrang == "Đóng gói" || inv.tinhTrang == "Mới tạo") ? true : false;
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
                if(inv.tinhTrang != "Đóng gói" && inv.tinhTrang != "Mới tạo")
                {
                    return await invoices.UpdateOneAsync(filter, denyUpdate);
                }
            }
            
            return await invoices.UpdateOneAsync(filter, acceptUpdate.Set("tinhTrangCu", invs[0].tinhTrangCu == "" ? (invs[0].tinhTrang) : (invs[0].tinhTrang += " -> Đã huỷ")));
        }
        public async Task<DonHang> ChangeInvoiceStatus(string id, DonHang donHangIn)
        {
            DonHang donHang = await invoices.Find(c => c.id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang.tinhTrangCu.Length == 0)
                donHang.tinhTrangCu = donHang.tinhTrang;
            else
                donHang.tinhTrangCu += (" -> " + donHang.tinhTrang);
            donHang.tinhTrang = donHangIn.tinhTrang;
            var updatedDonHang = await invoices.ReplaceOneAsync(c => c.id == id, donHang).ConfigureAwait(false);
            return donHang;
        }
        public async Task<List<DonHang>> GetAllDonHangForStatistic(string stordId)
        {
            return await invoices.Find<DonHang>(d => d.cuaHang == stordId && (d.tinhTrang == "Giao thành công" || d.tinhTrang == "Đã nhận hàng")).ToListAsync();
        }
        public Task<List<DonHang>> GetAll()
        {
            return invoices.Find(c => true).ToListAsync();
        }
        public async Task<DonHang> CreateAsync(DonHang donHang)
        {
            await invoices.InsertOneAsync(donHang).ConfigureAwait(false);
            return donHang;
        }
        public async Task<DonHang> Update(string id, DonHang donHangIn)
        {
            DonHang donHang = await invoices.Find(c => c.id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            donHangIn.id = donHang.id;
            if (donHangIn.thoiGianDat == null) donHangIn.thoiGianDat = donHang.thoiGianDat;
            if (donHangIn.tinhTrang == null) donHangIn.tinhTrang = donHang.tinhTrang;
            var updatedDonHang = await invoices.ReplaceOneAsync(c => c.id== id, donHangIn).ConfigureAwait(false);
            return donHangIn;
        }
        public async Task<DonHang> Update1(string id, string shipperid)
        {
            DonHang donHang = await invoices.Find(c => c.id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            donHang.tinhTrang = "Đang giao";
            donHang.shipper = shipperid;
            var updatedDonHang = await invoices.ReplaceOneAsync(c => c.id == id, donHang).ConfigureAwait(false);
            return donHang;
        }
        public async Task<DonHang> Update2(string id, string shipperid)
        {
            DonHang donHang = await invoices.Find(c => c.id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            donHang.tinhTrang = "Giao thành công";
            donHang.shipper = shipperid;
            var updatedDonHang = await invoices.ReplaceOneAsync(c => c.id == id, donHang).ConfigureAwait(false);
            return donHang;
        }
        public async Task<DonHang> Update3(string id, string shipperid)
        {
            DonHang donHang = await invoices.Find(c => c.id == id).FirstOrDefaultAsync().ConfigureAwait(false);
            donHang.tinhTrang = "Giao thất bại";
            donHang.shipper = shipperid;
            var updatedDonHang = await invoices.ReplaceOneAsync(c => c.id == id, donHang).ConfigureAwait(false);
            return donHang;
        }
        public async Task<DonHang> DeleteAsync(string id)
        {
            DonHang donHang = await invoices.Find(c => c.id.ToString() == id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (donHang == null)
            {
                return null;
            }
            var updatedDonHang = await invoices.DeleteOneAsync(c => c.id.ToString() == id).ConfigureAwait(false);
            return donHang;
        }
    }
}
