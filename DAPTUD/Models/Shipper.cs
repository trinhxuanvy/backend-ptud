using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class Shipper
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }   
        [BsonElement]
        public string cmnd { get; set; }
        [BsonElement]
        public string gioiTinh { get; set; }
        [BsonElement]
        public string hinhAnh { get; set; }
        [BsonElement]
        public string hinhAnhCMNDMatSau { get; set; }
        [BsonElement]
        public string hinhAnhCMNDMatTruoc { get; set; }
        [BsonElement]
        public string hoTen { get; set; }
        [BsonElement]
        public string matKhau { get; set; }
        [BsonElement]
        public string ngaySinh { get; set; }
        [BsonElement]
        public string sdt { get; set; }
        [BsonElement]
        public float doUyTin { get; set; }
        [BsonElement]
        public string email { get; set; }
        [BsonElement]
        public string diaChi { get; set; }
        [BsonElement]
        public object ViTri { get; set; }
        [BsonElement]
        public object phieuXetNghiem { get; set; }
        [BsonElement]
        public object tiemNgua { get; set; }
        [BsonElement]
        public int trangThaiHoatDong { get; set; }
    }
}
