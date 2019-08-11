using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Skills.API.Controllers;
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
        private Mock<IRepository<SkillDto>> _mockRepository;
        private Mock<IAdapter<SkillDto>> _mockAdapter;
        private Mock<ILogger<SkillsController>> _mockLogger;

        public SkillsControllerTests(ITestOutputHelper output)
        {
            _output = output;
            _mockRepository = new Mock<IRepository<SkillDto>>();
            _mockAdapter = new Mock<IAdapter<SkillDto>>();
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

        [Fact]
        public async Task Save_WhenCalled_ShouldWork()
        {
            var expected = "1234567890";
            var skillDto = new SkillDto {Id = expected};
            _mockRepository.Setup(_ => _.Save(It.IsAny<SkillDto>())).Returns(Task.FromResult(skillDto));
            var controller = new SkillsController(_mockRepository.Object, _mockAdapter.Object, _mockLogger.Object);
            var actual = await controller.Save(skillDto);
            Assert.IsAssignableFrom<ActionResult<SkillDto>>(actual);
            Assert.Contains(expected, JsonConvert.SerializeObject(actual));
        }

        [Fact]
        public async Task Update_WhenCalled_ShouldWork()
        {
            var expected = "1234567890";
            var skillDto = new SkillDto {Id = expected};
            _mockRepository.Setup(_ => _.Update(It.IsAny<SkillDto>())).Returns(Task.FromResult(skillDto));
            var controller = new SkillsController(_mockRepository.Object, _mockAdapter.Object, _mockLogger.Object);
            var actual = await controller.Update(skillDto);
            Assert.IsAssignableFrom<ActionResult<SkillDto>>(actual);
            Assert.Contains(expected, JsonConvert.SerializeObject(actual));
        }

        [Fact]
        public async Task Delete_WhenCalled_ShouldWork()
        {
            _mockRepository.Setup(
                _ => _.Delete(It.IsAny<string>())).Returns(Task.FromResult(HttpStatusCode.NoContent)
            );
            var controller = new SkillsController(_mockRepository.Object, _mockAdapter.Object, _mockLogger.Object);
            var actual = await controller.Delete("");
            Assert.IsAssignableFrom<ActionResult<HttpStatusCode>>(actual);
            Assert.Contains(
                ((int) HttpStatusCode.NoContent).ToString(), JsonConvert.SerializeObject(actual)
            );
        }
    }
}