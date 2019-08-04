using Skills.ValueObject;

namespace Skills.Entity
{
    public class Project : IEntity
    {
        public ObjectId Id { get; set; }
        public ProjectName Name { get; set; }
        public Slug Slug { get; set; }
        public string Logo { get; set; }
    }
}