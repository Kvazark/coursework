using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CourseworkAPIMongo.Models
{
    public class Creature
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Species { get; set; }

        public string Appellation { get; set; }

        public string Classification { get; set; }
        
        [BsonElement("index")]
        public int Index { get; set; }
    }
}