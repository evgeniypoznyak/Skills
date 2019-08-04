using System.Net;
using Skills.Entity;
using Skills.ValueObject;
using Skills.Adapter;

namespace Skills.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        T Find(ObjectId id);
//        IEnumerable<T> Find(UserId id);
           HttpStatusCode Save(SkillDto skillDto);
//        HttpStatusCode Update(ReportDto reportDto);
    }
}