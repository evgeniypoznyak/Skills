using System.Net;
using Skills.Domain.Entity;
using Skills.Infrastructure.Adapter;
using Skills.ValueObject;

namespace Skills.Domain.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        T Find(ObjectId id);
//        IEnumerable<T> Find(UserId id);
           HttpStatusCode Save(SkillDto skillDto);
//        HttpStatusCode Update(ReportDto reportDto);
    }
}