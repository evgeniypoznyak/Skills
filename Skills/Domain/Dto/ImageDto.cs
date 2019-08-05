using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Skills.Domain.Dto
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ProjectImagesDto
    {
        [JsonProperty("label")]
        [BsonElement("label")]
        public string Label { get; set; }

        [JsonProperty("path")]
        [BsonElement("path")]
        public string Path { get; set; }
    }
}