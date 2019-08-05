using System.Net;
using Skills.Domain.Dto;
using Skills.Domain.Entity;
using Skills.ValueObject;

namespace Skills.Domain.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        T Find(string id);
        HttpStatusCode Save(SkillDto skillDto);
    }
}