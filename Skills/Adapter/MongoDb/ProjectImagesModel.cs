using MongoDB.Bson.Serialization.Attributes;

namespace Skills.Adapter.MongoDb
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ProjectImagesModel
    {
        public string label { get; set; }
        public string path { get; set; }
    }
}