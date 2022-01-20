using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class CuaHang
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string chuCuaHang { get; set; }

        public string diaChi { get; set; }

        public string giayChungNhanAnToan { get; set; }

        public string giayPhepKinhDoanh { get; set; }

        public string maSoThue { get; set; }

        public string tenCuaHang { get; set; }

        public int trangThai { get; set; }

        public double doUyTin { get; set; }
    }
}
