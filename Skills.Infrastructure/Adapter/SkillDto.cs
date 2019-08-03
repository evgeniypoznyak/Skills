using System.Collections.Generic;

namespace Skills.Infrastructure.Adapter
{
    public class SkillDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Vertion { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public List<ProjectDto> Projects { get; set; }
    }
}