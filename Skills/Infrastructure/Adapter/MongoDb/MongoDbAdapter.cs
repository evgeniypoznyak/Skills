using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Skills.Domain.Dto;

namespace Skills.Infrastructure.Adapter.MongoDb
{
    public class MongoDbAdapter : IAdapter
    {
        private readonly ILogger<MongoDbAdapter> _logger;
        private readonly string _databaseName = Environment.GetEnvironmentVariable("MONGO_SKILLS_DB_NAME");
        private readonly string _collectionName = Environment.GetEnvironmentVariable("MONGO_SKILLS_COLLECTION");
        private IMongoDatabase _db;
        private IMongoCollection<SkillDto> _collection;

        public MongoDbAdapter(IMongoClient mongoClient, ILogger<MongoDbAdapter> logger)
        {
            _logger = logger;
            _db = mongoClient.GetDatabase(_databaseName);
            _collection = _db.GetCollection<SkillDto>(_collectionName);
        }

        public async Task<SkillListDto> FindAll()
        {
            var documents = await _collection.Find(new BsonDocument()).ToListAsync();
            _logger.LogInformation("FindAll documents: {@documents}", documents);
            var skillList = new SkillListDto {Skills = documents};
            _logger.LogInformation("Returning back skillList: {@skillList}", skillList);
            return skillList;
        }
    }
}