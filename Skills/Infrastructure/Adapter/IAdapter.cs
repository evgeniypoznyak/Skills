using System.Threading.Tasks;
using Skills.Domain.Dto;

namespace Skills.Infrastructure.Adapter
{
    public interface IAdapter
    {
        Task<SkillListDto> FindAll();
    }
}