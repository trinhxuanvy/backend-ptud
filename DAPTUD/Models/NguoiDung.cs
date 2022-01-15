using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class NguoiDung
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string id { get; set; }
        public string cmnd { get; set; }
        public string hinhAnh { get; set; }
        public string hinhAnhCMNDMatSau { get; set; }
        public string hinhAnhCMNDMatTruoc { get; set; }
        public string hoTen { get; set; }
        public int loaiND { get; set; }
        public int matKhau { get; set; }
        public int ngaySinh { get; set; }
        public double doUyTin { get; set; }
        public string email { get; set; }
        public string diaChi { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Array)]
        public Array gioHang { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.Array)]
        public Array diaChiGiaoNhan { get; set; }
    }
}
