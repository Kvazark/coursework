using System.Collections.Generic;
using CourseworkAPIMongo.Models;
using MongoDB.Driver;

namespace CourseworkAPIMongo.Services
{
    public class CreatureService
    {
        private readonly IMongoCollection<Creature> _creatures;

        public CreatureService(CourseworkDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _creatures = database.GetCollection<Creature>(settings.CreaturesCollectionName);
        }

        public List<Creature> Get() =>
            _creatures.Find(creature => true).ToList();

        public Creature Get(string id) =>
            _creatures.Find<Creature>(creature => creature.Id == id).FirstOrDefault();

        public Creature Create(Creature creature)
        {
            _creatures.InsertOne(creature);
            return creature;
        }

        public void Update(string id, Creature creatureIn) =>
            _creatures.ReplaceOne(creature => creature.Id == id, creatureIn);

        public void Remove(Creature creatureIn) =>
            _creatures.DeleteOne(creature => creature.Id == creatureIn.Id);

        public void Remove(string id) => 
            _creatures.DeleteOne(creature => creature.Id == id);
    }
}