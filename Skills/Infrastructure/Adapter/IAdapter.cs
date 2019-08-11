using System.Threading.Tasks;
using Skills.Domain.Dto;

namespace Skills.Infrastructure.Adapter
{
    public interface IAdapter<T> where T : ISkillDto
    {
        Task<SkillListDto> FindAll();
        Task<T> Save(T skillDto);
    }
}