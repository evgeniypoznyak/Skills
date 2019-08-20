using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Skills.Domain.Dto;

namespace Skills.Infrastructure.Adapter.MongoDb
{
    public class MongoDbAdapter : IAdapter<SkillDto>
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
            _logger.LogInformation("FindAll documents count: {@documents}", documents.Count);
            var skillList = new SkillListDto {Skills = documents};
            _logger.LogInformation("Returning back skillList count: {@count}", skillList.Skills.Count);
            return skillList;
        }

        public async Task<SkillDto> Save(SkillDto skillDto)
        {
            _logger.LogInformation("MongoDbAdapter: Processing request from repository");
            await _collection.InsertOneAsync(skillDto);
            return skillDto;
        }

        public async Task<SkillDto> Update(SkillDto skillDto)
        {
            _logger.LogInformation("MongoDbAdapter: Processing request from repository");
            await _collection.ReplaceOneAsync(_ => _.Id == skillDto.Id, skillDto);
            return skillDto;
        }

        public async Task<SkillListDto> Update(SkillListDto skillListDto)
        {
            var documents = await _collection.Find(new BsonDocument()).ToListAsync();
            _logger.LogInformation("MongoDbAdapter: Processing request from repository");
            foreach (SkillDto skillDto in documents)
            {
                await _collection.DeleteManyAsync(_ => _.Id == skillDto.Id);
            }
            await _collection.InsertManyAsync(skillListDto.Skills);
            return skillListDto;
        }

        public async Task<HttpStatusCode> Delete(string skillId)
        {
            _logger.LogInformation("MongoDbAdapter: Processing request from repository");
            var deleteOneAsync = await _collection.DeleteOneAsync(_ => _.Id == skillId);
            if (deleteOneAsync.DeletedCount == 0)
                throw new Exception($"Unable to delete data for provided skill id: {skillId}");
            _logger.LogInformation("Skill was successfully deleted");
            return HttpStatusCode.NoContent;
        }
    }
}