using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class VanDon
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement]
        public ObjectId maDonHang { get; set; }
        [BsonElement]
        public string sdtNguoiMua { get; set; }
        [BsonElement]
        public ObjectId cuaHang { get; set; }
        [BsonElement]
        public string sdtShipper { get; set; }
        [BsonElement]
        public string trangThai { get; set; }
        [BsonElement]
        public string diaChiGiaoHang { get; set; }
        [BsonElement]
        public int giaTri { get; set; }
        [BsonElement]
        public DateTime thoiGianDat { get; set; }
        [BsonElement]
        public DateTime thoiGianGiao { get; set; } 
    }
}
