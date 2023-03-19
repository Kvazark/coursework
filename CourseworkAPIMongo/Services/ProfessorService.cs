using System.Collections.Generic;
using CourseworkAPIMongo.Models;
using MongoDB.Driver;

namespace CourseworkAPIMongo.Services
{
    public class ProfessorService
    {
        private readonly IMongoCollection<Professor> _professors;

        public ProfessorService(CourseworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _professors = database.GetCollection<Professor>(settings.ProfessorsCollectionName);
        }

        public List<Professor> Get() =>
            _professors.Find(professor => true).ToList();

        public Professor Get(string id) =>
            _professors.Find<Professor>(professor => professor.Id == id).FirstOrDefault();

        public Professor Create(Professor professor)
        {
            _professors.InsertOne(professor);
            return professor;
        }

        public void Update(string id, Professor professorIn) =>
            _professors.ReplaceOne(professor => professor.Id == id, professorIn);

        public void Remove(Professor professorIn) =>
            _professors.DeleteOne(professor => professor.Id == professorIn.Id);

        public void Remove(string id) => 
            _professors.DeleteOne(professor => professor.Id == id);
    }
}