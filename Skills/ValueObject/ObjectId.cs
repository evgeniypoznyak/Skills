using System;

namespace Skills.ValueObject
{
    public class ObjectId
    {
        public Guid Id { get; }

        public ObjectId(Guid id)
        {
            Id = id;
        }

        public ObjectId (string id)
        {
            Id = Guid.Parse(id);
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}