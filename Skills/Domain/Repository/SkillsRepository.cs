using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Skills.Domain.Dto;
using Skills.Infrastructure.Adapter;

namespace Skills.Domain.Repository
{
    public class SkillsRepository: IRepository<SkillDto>
    {
        private readonly IAdapter<SkillDto> _adapter;
        private readonly IMapper _mapper;
        private readonly ILogger<SkillsRepository> _logger;

        public SkillsRepository(IAdapter<SkillDto> adapter, IMapper mapper,  ILogger<SkillsRepository> logger)
        {
            _adapter = adapter;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<SkillDto> Find(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SkillDto> Save(SkillDto skillDto)
        {
            return await _adapter.Save(skillDto);
        }
        
        public async Task<SkillDto> Update(SkillDto skillDto)
        {
            return await _adapter.Update(skillDto);
        }
        
        public async Task<HttpStatusCode> Delete(string skillId)
        {
            return await _adapter.Delete(skillId);
        }
    }
}