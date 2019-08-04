using MongoDB.Bson.Serialization.Attributes;

namespace Skills.Adapter.MongoDb
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class SkillModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string panel { get; set; }
        public string version { get; set; }
        public string logo { get; set; }
        public string order { get; set; }
        public string description { get; set; }
        public ProjectModel [] projects { get; set; }
    }
}