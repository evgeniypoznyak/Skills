using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Skills.Domain.Dto
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ProjectDto
    {
        [JsonProperty("id")]
        [BsonElement("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("companyName")]
        [BsonElement("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("website")]
        [BsonElement("website")]
        public string Website { get; set; }

        [JsonProperty("github")]
        [BsonElement("github")]
        public string Github { get; set; }

        [JsonProperty("logo")]
        [BsonElement("logo")]
        public string Logo { get; set; }

        [JsonProperty("dateCreated")]
        [BsonElement("dateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("cardCover")]
        [BsonElement("cardCover")]
        public string CardCover { get; set; }

        [JsonProperty("images")]
        [BsonElement("images")]
        public ProjectImagesDto[] Images { get; set; }

        [JsonProperty("shortDescription")]
        [BsonElement("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("longDescription")]
        [BsonElement("longDescription")]
        public LongDescriptionDto[] LongDescription { get; set; }
    }
}