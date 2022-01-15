using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class DonHang
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }

        public string tinhTrang { get; set; }

        public DateTime thoiGianDat { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string nguoiMua { get; set; }

        public string phuongThucThanhToan { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string shipper { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string cuaHang { get; set; }

        public int danhGiaCuaKH { get; set; }

        public int danhGiaCuaNhaCC { get; set; }
        
        public double tongTien { get; set; }

        public int khDanhGiaShipper { get; set; }

        public string tinhTrangCu { get; set; }
    }
}
