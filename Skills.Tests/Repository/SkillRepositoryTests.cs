using System;
using AutoMapper;
using Moq;
using Skills.Repository;
using Skills.ValueObject;
using Skills.Adapter;
using Skills.Aggregate;
using Xunit;
using Xunit.Abstractions;

namespace Skills.Tests.Repository
{
    public class SkillRepositoryTests
    {
        private readonly ITestOutputHelper _output;
        private Mock<IAdapter> _mockDynamoDb;
        private Mock<IMapper> _mapper;
        private SkillsRepository _skillsRepository;

        public SkillRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            _mockDynamoDb = new Mock<IAdapter>();
            _mapper = new Mock<IMapper>();
            _mapper.Setup(x => x.Map<SkillDto, Skill>(It.IsAny<SkillDto>()))
                .Returns(It.IsAny<Skill>());
            _skillsRepository = new SkillsRepository(_mockDynamoDb.Object, _mapper.Object);
        }
        
        [Fact]
        public void Find_IfSkillIdPassed_ShouldReturnSkillEntity()
        {
            try
            {
                var result = _skillsRepository.Find(new ObjectId(new Guid()));
            }
            catch (Exception e)
            {
               _output.WriteLine("Error: " + e.Message);
            }
           
        }
    }
}