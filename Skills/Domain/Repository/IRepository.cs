using System.Threading.Tasks;
using Skills.Domain.Dto;

namespace Skills.Domain.Repository
{
    public interface IRepository<T> where T : ISkillDto
    {
        Task<T> Find(string id);
        Task<T> Save(SkillDto skillDto);
        Task<T> Update(SkillDto skillDto);
    }
}