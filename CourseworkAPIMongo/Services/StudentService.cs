using System.Collections.Generic;
using System.Threading.Tasks;
using CourseworkAPIMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CourseworkAPIMongo.Services
{
    public class StudentService
    {
        private readonly IMongoCollection<Student> _studentCollection;
        
        public StudentService(
            IOptions<CourseworkDatabaseSettings> courseworkDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                courseworkDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                courseworkDatabaseSettings.Value.DatabaseName);

            _studentCollection = mongoDatabase.GetCollection<Student>(
                courseworkDatabaseSettings.Value.StudentsCollectionName);
        }
        // </snippet_ctor>

        public async Task<List<Student>> GetAsync() =>
            await _studentCollection.Find(_ => true).ToListAsync();

        public async Task<Student?> GetAsync(string id) =>
            await _studentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Student newStudent) =>
            await _studentCollection.InsertOneAsync(newStudent);

        public async Task UpdateAsync(string id, Student updatedStudent) =>
            await _studentCollection.ReplaceOneAsync(x => x.Id == id, updatedStudent);

        public async Task RemoveAsync(string id) =>
            await _studentCollection.DeleteOneAsync(x => x.Id == id);
        public async Task<Student?> AggregateAsync(int course) =>
            await _studentCollection.Aggregate()
                .Match(e=>e.Course == course)
                .FirstOrDefaultAsync();
    }
}