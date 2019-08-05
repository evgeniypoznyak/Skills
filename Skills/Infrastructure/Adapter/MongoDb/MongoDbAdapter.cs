using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Skills.Infrastructure.Adapter.MongoDb
{
    public class MongoDbAdapter : IAdapter
    {
        private readonly ILogger<MongoDbAdapter> _logger;
        private readonly string _databaseName = Environment.GetEnvironmentVariable("MONGO_SKILLS_DB_NAME");
        private readonly string _collectionName = Environment.GetEnvironmentVariable("MONGO_SKILLS_COLLECTION");
        private IMongoDatabase _db;
        private IMongoCollection<SkillModel> _collection;

        public MongoDbAdapter(IMongoClient mongoClient,  ILogger<MongoDbAdapter> logger)
        {
            _logger = logger;
            _db = mongoClient.GetDatabase(_databaseName);
            _collection = _db.GetCollection<SkillModel>(_collectionName);
        }

        public List<object> FindAll()
        {
            var documents = _collection.Find(new BsonDocument()).ToListAsync().Result;
           
//            var result = _collection.Find(s => true).ToListAsync().Result;
            return JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(documents));

//            return GetSkillDtoListFromSkillModelList(result);
        }

//
//        public List<SkillModel> GetSkillDtoListFromSkillModelList(List<SkillModel> list)
//        {
//            var result = new List<SkillDto>();
//            foreach (SkillModel skillModel in list)
//            {
//                var projects = new List<ProjectDto>();
//                foreach (ProjectModel projectModel in skillModel.Projects)
//                {
//                    var projectDto = new ProjectDto
//                    {
//                        Id = projectModel.Id,
//                        Name = projectModel.Name,
//                        Logo = projectModel.Logo,
//                        Url = projectModel.Url,
//                        Repository = projectModel.Repository,
////                        Images = projectModel.Images,
//                        Videos = projectModel.Videos,
//                        Description = projectModel.Description
//                    };
//                    projects.Add(projectDto);
//                }
//                var skillDto = new SkillDto
//                {
//                    Id = skillModel.Id,
//                    Name = skillModel.Name,
//                    Vertion = skillModel.Version,
////                    Slug = skillModel.Slug,
//                    Description = skillModel.Description,
//                    Logo = skillModel.Logo,
//                    Projects = projects
//                };
//                result.Add(skillDto);
//            }
//
//            return result;
//        }
    }
}