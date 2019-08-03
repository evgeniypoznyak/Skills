using MongoDB.Bson.Serialization.Attributes;

namespace Skills.Infrastructure.Adapter.MongoDb
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ProjectImagesModel
    {
        public string label { get; set; }
        public string path { get; set; }
    }
}