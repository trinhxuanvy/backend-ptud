using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Entities
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
    }
}
