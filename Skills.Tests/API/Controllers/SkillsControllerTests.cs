using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Skills.API.Controllers;
using Skills.Domain.Aggregate;
using Skills.Domain.Dto;
using Skills.Domain.Repository;
using Skills.Infrastructure.Adapter;
using Xunit;
using Xunit.Abstractions;

namespace Skills.Tests.API.Controllers
{
    public class SkillsControllerTests
    {
        private readonly ITestOutputHelper _output;
        private Mock<IRepository<Skill>> _mockRepository;
        private Mock<IAdapter> _mockAdapter;
        private Mock<ILogger<SkillsController>> _mockLogger;

        public SkillsControllerTests(ITestOutputHelper output)
        {
            _output = output;
            _mockRepository = new Mock<IRepository<Skill>>();
            _mockAdapter = new Mock<IAdapter>();
            _mockLogger = new Mock<ILogger<SkillsController>>();
        }

        [Fact]
        public async Task Find_WhenCalled_ShouldWork()
        {
            var expected = "1234567890";
            _mockAdapter.Setup(_ => _.FindAll()).Returns(Task.FromResult(
                new SkillListDto {Skills = new List<SkillDto> {new SkillDto {Id = expected}}}
            ));
            var controller = new SkillsController(_mockRepository.Object, _mockAdapter.Object, _mockLogger.Object);
            var actual = await controller.Find();
            Assert.IsAssignableFrom<ActionResult<SkillListDto>>(actual);
            Assert.Contains(expected, JsonConvert.SerializeObject(actual));
        }
    }
}