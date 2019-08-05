using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Skills.Domain.Aggregate;
using Skills.Infrastructure.Adapter;
using Skills.ValueObject;

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

//        public IEnumerable<Skill> FindAll()
//        {
//            var listOfSkillsDto = _adapter.FindAll();
//            var collection = new List<Skill>();
//            foreach (var skillDto in listOfSkillsDto)
//            {
//                collection.Add(_mapper.Map<SkillDto, Skill>(skillDto));
//            }
//            return collection;
//        }
        
        public Skill MapSkillDtoToEntity(SkillDto skillDto)
        {
            return _mapper.Map<SkillDto, Skill>(skillDto);
        }

        public Skill Find(ObjectId id)
        {
            return new Skill();
        }

        public HttpStatusCode Save(SkillDto skillDto)
        {
            return HttpStatusCode.Created;
        }
    }
}