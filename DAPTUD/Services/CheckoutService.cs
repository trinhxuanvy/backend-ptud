using DAPTUD.IDbConfig;
using DAPTUD.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Services
{
    public class CheckoutModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public List<ProductCustom> product { get; set; }
        public string store { get; set; }
        public int total { get; set; }
    }
    public class CheckoutService
    {
        private readonly IMongoCollection<NguoiDung> cus;
        private readonly IMongoCollection<SanPham> prod;
        public CheckoutService(IDatabaseConfig dbConfig)
        {
            var client = new MongoClient(dbConfig.ConnectionString);
            var database = client.GetDatabase(dbConfig.DatabaseName);
            cus = database.GetCollection<NguoiDung>(dbConfig.NguoiDungCollectionName);
            prod = database.GetCollection<SanPham>(dbConfig.SanPhamCollectionName);
        }
        public async Task<CheckoutModel> GetCartById(string id)
        {
            CheckoutModel result = new CheckoutModel();
            NguoiDung customer = await cus.Find<NguoiDung>(s => s.id == id).FirstOrDefaultAsync();
            //customer.
            List<ProductCustom> cart = new List<ProductCustom>();
            int total = 0;
            for (int i = 0; i < customer.gioHang.Length; i++)
            {
                if (customer.gioHang[i].sanPham == "" || customer.gioHang[i].sanPham == null) { continue; }
                ProductCustom tmp = new ProductCustom();
                SanPham product = await prod.Find<SanPham>(s => s.id == customer.gioHang[i].sanPham).FirstOrDefaultAsync();

                
                tmp.productid = product.id;
                tmp.store = product.cuaHang;
                tmp.price = product.giaTien;
                tmp.numOfElement = customer.gioHang[i].soLuong;
                tmp.unit = product.donViTinh;
                tmp.total = tmp.price * tmp.numOfElement;
                tmp.product = product.tenSanPham;
                cart.Add(tmp);
                total += tmp.total;
            }

            string ho = "";
            string ten = "";
            if (customer.hoTen.IndexOf(' ') <= 0)
            {
                ten = customer.hoTen;
            }
            else
            {
                ho = customer.hoTen.Substring(0, customer.hoTen.IndexOf(' '));
                ten = customer.hoTen.Substring(customer.hoTen.IndexOf(' ') + 1);
            }

            result.lastName = ho;
            result.firstName = ten;

            for (int i = 0; i < customer.diaChiGiaoNhan.Length; i++)
            {
                if (customer.diaChiGiaoNhan[i].diaChiMacDinh == 1)
                {
                    result.address = customer.diaChiGiaoNhan[i].diaChi;
                }
            }

            if(cart.Count> 0) { result.store = cart[0].store; }
            else { result.store = null; }
            result.phoneNumber = customer.sdt;
            result.product = cart;
            result.total = total;
            return result;
        }
    }
}
