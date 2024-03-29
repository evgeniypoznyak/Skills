using System;

namespace Skills.ValueObject
{
    public class ProjectId
    {
        public Guid Id { get; }

        public ProjectId(Guid id)
        {
            Id = id;
        }

        public ProjectId (string id)
        {
            Id = Guid.Parse(id);
        }
    }
}