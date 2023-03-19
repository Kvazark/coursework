using System.Collections.Generic;
using System.Threading.Tasks;
using CourseworkAPIMongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CourseworkAPIMongo.Services
{
    public class CreatureService
    {
        private readonly IMongoCollection<Creature> _creatureCollection;
        
        public CreatureService(
            IOptions<CourseworkDatabaseSettings> courseworkDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                courseworkDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                courseworkDatabaseSettings.Value.DatabaseName);

            _creatureCollection = mongoDatabase.GetCollection<Creature>(
                courseworkDatabaseSettings.Value.CreaturesCollectionName);
        }
        // </snippet_ctor>

        public async Task<List<Creature>> GetAsync() =>
            await _creatureCollection.Find(_ => true).ToListAsync();

        public async Task<Creature?> GetAsync(string id) =>
            await _creatureCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Creature newCreature) =>
            await _creatureCollection.InsertOneAsync(newCreature);

        public async Task UpdateAsync(string id, Creature updatedCreature) =>
            await _creatureCollection.ReplaceOneAsync(x => x.Id == id, updatedCreature);

        public async Task RemoveAsync(string id) =>
            await _creatureCollection.DeleteOneAsync(x => x.Id == id);
    }
}