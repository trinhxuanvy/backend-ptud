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
        public DateTime thoiGianDat { get; set; }
        public string nguoiMua { get; set; }
        public int tinhTrang { get; set; }
    }
}
