using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class SanPham
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }

        public string tenSanPham { get; set; }

        public string xuatXu { get; set; }

        public int giaTien { get; set; }

        public DateTime hanSuDung { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string cuaHang { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string loaiHang { get; set; }
        public string hinhAnh { get; set; }
        public bool thietYeu { get; set; }
        public string tenCuaHang { get; set; }
        public string tenLoaiHang { get; set; }
        public string donViTinh { get; set; }
    }
}
