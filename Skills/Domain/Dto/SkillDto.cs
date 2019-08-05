using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Skills.Domain.Dto
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class SkillDto
    {
        [JsonProperty("id")]
        [BsonElement("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("panel")]
        [BsonElement("panel")]
        public string Panel { get; set; }

        [JsonProperty("version")]
        [BsonElement("version")]
        public string Version { get; set; }

        [JsonProperty("logo")]
        [BsonElement("logo")]
        public string Logo { get; set; }

        [JsonProperty("order")]
        [BsonElement("order")]
        public string Order { get; set; }

        [JsonProperty("description")]
        [BsonElement("description")]
        public string Description { get; set; }

        [JsonProperty("projects")]
        [BsonElement("projects")]
        public ProjectDto[] Projects { get; set; }
    }
}