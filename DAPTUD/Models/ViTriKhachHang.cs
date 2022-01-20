using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class ViTriKhachHang
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }

        public double latitude { get; set; }

        public double longtitude { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string objectId { get; set; }
    }
}
