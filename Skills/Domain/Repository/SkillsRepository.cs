using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Skills.Domain.Aggregate;
using Skills.Domain.Dto;
using Skills.Infrastructure.Adapter;

namespace Skills.Domain.Repository
{
    public class SkillsRepository: IRepository<Skill>
    {
        private readonly IAdapter _adapter;
        private readonly IMapper _mapper;
        private readonly ILogger<SkillsRepository> _logger;

        public SkillsRepository(IAdapter adapter, IMapper mapper,  ILogger<SkillsRepository> logger)
        {
            _adapter = adapter;
            _mapper = mapper;
            _logger = logger;
        }

        public Skill Find(string id)
        {
            throw new System.NotImplementedException();
        }

        public HttpStatusCode Save(SkillDto skillDto)
        {
            throw new System.NotImplementedException();
        }
    }
}