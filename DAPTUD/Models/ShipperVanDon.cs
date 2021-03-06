using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class ShipperVanDon
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string vandonid { get; set; }

        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string shipper { get; set; }
        public double khoangcach { get; set; }
        public string trangthai { get; set; }
    }
}
