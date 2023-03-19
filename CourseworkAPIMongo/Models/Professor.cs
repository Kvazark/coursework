using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CourseworkAPIMongo.Models
{
    public class Professor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string ProfessorName { get; set; }
        [BsonElement("Surname")]
        public string ProfessorSurname { get; set; }

        public int Age { get; set; }

        public string Discipline { get; set; }
        
        [BsonElement("index")]
        public int Index { get; set; }

    }
}