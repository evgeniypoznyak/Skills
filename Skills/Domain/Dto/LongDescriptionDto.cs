using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Skills.Domain.Dto
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class LongDescriptionDto
    {
        [JsonProperty("picture")]
        [BsonElement("picture")]
        public string Picture { get; set; }

        [JsonProperty("description")]
        [BsonElement("description")]
        public string Description { get; set; }
    }
}