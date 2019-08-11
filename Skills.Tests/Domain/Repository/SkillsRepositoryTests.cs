using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Skills.Domain.Dto;
using Skills.Domain.Repository;
using Skills.Infrastructure.Adapter;
using Xunit;
using Xunit.Abstractions;

namespace Skills.Tests.Domain.Repository
{
    public class SkillsRepositoryTests
    {
        private readonly ITestOutputHelper _output;
        private Mock<IAdapter<SkillDto>> _mockAdapter;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<SkillsRepository>> _mockLogger;

        public SkillsRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            _mockAdapter = new Mock<IAdapter<SkillDto>>();
            _mockLogger = new Mock<ILogger<SkillsRepository>>();
            _mockMapper = new Mock<IMapper>();
        }
        
        [Fact]
        public async Task Save_WhenCalled_ShouldWork()
        {
            var expected = "1234567890";
            var skillDto = new SkillDto {Id = expected};
            _mockAdapter.Setup(_ => _.Save(It.IsAny<SkillDto>())).Returns(Task.FromResult(skillDto));
            var repository = new SkillsRepository(_mockAdapter.Object, _mockMapper.Object, _mockLogger.Object);
            var actual = await repository.Save(skillDto);
            Assert.IsAssignableFrom<SkillDto>(actual);
            Assert.Equal(expected, actual.Id);
        }     
        
        [Fact]
        public async Task Update_WhenCalled_ShouldWork()
        {
            var expected = "1234567890";
            var skillDto = new SkillDto {Id = expected};
            _mockAdapter.Setup(_ => _.Update(It.IsAny<SkillDto>())).Returns(Task.FromResult(skillDto));
            var repository = new SkillsRepository(_mockAdapter.Object, _mockMapper.Object, _mockLogger.Object);
            var actual = await repository.Update(skillDto);
            Assert.IsAssignableFrom<SkillDto>(actual);
            Assert.Equal(expected, actual.Id);
        }
    }
}