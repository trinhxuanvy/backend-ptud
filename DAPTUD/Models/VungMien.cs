using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAPTUD.Models
{
    public class VungMien
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement]
        public string huyen { get; set; }
        [BsonElement]
        public string thanhPho { get; set; }
        [BsonElement]
        public int capDoDich { get; set; }
    }
}
