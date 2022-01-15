using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class ProductCustom
    {
        public string product { get; set; }
        public int numOfElement { get; set; }
        public string unit { get; set; }
        public int price { get; set; }
        public int total { get; set; }
    }
    public class Cart
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string sanPham { get; set; }
        public string tenSanPham { get; set; }
        public int soLuong { get; set; }
        public int tongTien { get; set; }
    }
    public class Address
    {
        public string diaChi { get; set; }
        public int diaChiMacDinh { get; set; }
    }
    public class NguoiDung
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string id { get; set; }
        public string cmnd { get; set; }
        public string gioiTinh { get; set; }
        public string hinhAnh { get; set; }
        public string hinhAnhCMNDMatSau { get; set; }
        public string hinhAnhCMNDMatTruoc { get; set; }
        public string hoTen { get; set; }
        public int loaiND { get; set; }
        public string matKhau { get; set; }
        public string ngaySinh { get; set; }
        public string sdt { get; set; }
        public double doUyTin { get; set; }
        public string email { get; set; }
        public string diaChi { get; set; }
        public Cart[] gioHang { get; set; }
        public Address[] diaChiGiaoNhan { get; set; }
    }
}
