using MongoDB.Bson.Serialization.Attributes;

namespace Skills.Infrastructure.Adapter.MongoDb
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ProjectLongDescriptionModel
    {
        public string picture { get; set; }
        public string description { get; set; }
    }
}