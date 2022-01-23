using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class ChiTietDonHang
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string id { get; set; }
        public int soLuong { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string donHang { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string sanPham { get; set; }

        public string danhGia { get; set; }

        public string phanHoi { get; set; }
    }
}
