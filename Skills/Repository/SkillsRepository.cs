using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Skills.Domain.Entity;
using Skills.Domain.ValueObject;
using Skills.Infrastructure.Adapter;

namespace Skills.Domain.Repository
{
    public class SkillsRepository: IRepository<Skill>
    {
        private readonly IAdapter _adapter;
        private readonly IMapper _mapper;

        public SkillsRepository(IAdapter adapter, IMapper mapper)
        {
            _adapter = adapter;
            _mapper = mapper;
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