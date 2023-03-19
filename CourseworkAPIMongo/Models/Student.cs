using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CourseworkAPIMongo.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string StudentName { get; set; }
        [BsonElement("Surname")]
        public string StudentSurname { get; set; }

        public decimal Course { get; set; }

        public string LearnedSpells { get; set; }

        public string Faculty { get; set; }
    }
}