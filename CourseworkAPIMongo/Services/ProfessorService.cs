using System.Collections.Generic;
using System.Threading.Tasks;
using CourseworkAPIMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CourseworkAPIMongo.Services
{
    public class ProfessorService
    {
        private readonly IMongoCollection<Professor> _professorCollection;
        
        public ProfessorService(
            IOptions<CourseworkDatabaseSettings> courseworkDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                courseworkDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                courseworkDatabaseSettings.Value.DatabaseName);

            _professorCollection = mongoDatabase.GetCollection<Professor>(
                courseworkDatabaseSettings.Value.ProfessorsCollectionName);
        }
        // </snippet_ctor>

        public async Task<List<Professor>> GetAsync() =>
            await _professorCollection.Find(_ => true).ToListAsync();

        public async Task<Professor?> GetAsync(string id) =>
            await _professorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Professor newProfessor) =>
            await _professorCollection.InsertOneAsync(newProfessor);

        public async Task UpdateAsync(string id, Professor updatedProfessor) =>
            await _professorCollection.ReplaceOneAsync(x => x.Id == id, updatedProfessor);

        public async Task RemoveAsync(string id) =>
            await _professorCollection.DeleteOneAsync(x => x.Id == id);
        public async Task<Professor?> AggregateAsync(int age) =>
            await _professorCollection.Aggregate()
                .Match(e=>e.Age == age)
                .FirstOrDefaultAsync();
    }
}