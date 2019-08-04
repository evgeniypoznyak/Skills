using Skills.Entity;
using Skills.ValueObject;

namespace Skills.Aggregate
{
    public class Skill : IEntity
    {
        public ObjectId Id { get; set; }
        public ObjectName Name { get; set; }
        public string Version { get; set; }
        public string Slug { get; set; }
        public string Logo { get; set; }
        public string Order { get; set; }
        public string Description { get; set; }
        public Project [] Projects { get; set; }
    }
}