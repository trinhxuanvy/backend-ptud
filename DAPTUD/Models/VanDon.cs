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
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string maDonHang { get; set; }
        
        public string sdtNguoiMua { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string cuaHang { get; set; }
        public string sdtShipper { get; set; }
        public string trangThai { get; set; }
        public string diaChiGiaoHang { get; set; }
        public Int32 giaTri { get; set; }
        public DateTime thoiGianDat { get; set; }
        public DateTime thoiGianGiao { get; set; }
        public Int32 tienVanChuyen { get; set; }
    }
}
