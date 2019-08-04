using MongoDB.Bson.Serialization.Attributes;

namespace Skills.Infrastructure.Adapter.MongoDb
{
    [BsonIgnoreExtraElements]
    [BsonNoId]
    public class ProjectModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string companyName { get; set; }
        public string website { get; set; }
        public string github { get; set; }
        public string logo { get; set; }
        public string dateCreated { get; set; }
        public string cardCover { get; set; }
        public ProjectImagesModel[] images { get; set; }
        public string shortDescription { get; set; }
        public ProjectLongDescriptionModel[] longDescription { get; set; }
    }
}