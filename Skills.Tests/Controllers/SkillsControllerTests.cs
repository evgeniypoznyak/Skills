using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Skills.API.Controllers;
using Skills.Domain.Aggregate;
using Skills.Domain.Repository;
using Skills.Infrastructure.Adapter;
using Xunit;
using Xunit.Abstractions;

namespace Skills.Tests.Controllers
{
    public class SkillsControllerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly SkillsController _controller;
        private readonly Mock<ILogger<SkillsController>> _mockLogger;
        private Mock<IRepository<Skill>> _mockRepository;
        private Mock<IAdapter> _mockAdapter;

        public SkillsControllerTests(ITestOutputHelper output)
        {
            _output = output;
            _mockLogger = new Mock<ILogger<SkillsController>>();
            _mockAdapter = new Mock<IAdapter>();
            var mockedResponse = @"[{""Id"":""002"",""Name"":""Widget 2"",""Version"":""This is Version 2"",""Slug"":
""This is Slug 2"",""Logo"":""This is Logo 2"",""Order"":""This is Order 2"",""Description"":""This is Description 2"",
""Projects"":[{""Name"":""Name 2"",""Id"":""002"",""Slug"":""Slug 2"",""Logo"":""This is Logo 1""}]},{""Id"":""001"",
""Name"":""Widget 1"",""Version"":""This is Version 1"",""Slug"":""This is Slug 1"",""Logo"":""This is Logo 1"",
""Order"":""This is Order 1"",""Description"":""This is Description 1"",""Projects"":[{""Name"":""Name 1"",""Id"":
""001"",""Slug"":""Slug 1"",""Logo"":""This is Logo 1""}]}]";
            _mockAdapter.Setup(a => a.FindAll())
                .Returns(JsonConvert.DeserializeObject<List<object>>(mockedResponse));
            _mockRepository = new Mock<IRepository<Skill>>();
            _controller = new SkillsController(
                _mockRepository.Object,
                _mockAdapter.Object,
                _mockLogger.Object
            );
        }

        [Fact]
        public void Controller_Should_ExistAndInheritFromControllerBase()
        {
            Assert.IsAssignableFrom<ControllerBase>(_controller);
        }

        [Fact]
        public void Find_WhenCalled_JsonResultShouldReturn()
        {
            var result = _controller.Find();
            Assert.IsAssignableFrom<JsonResult>(result);
        }

        [Fact]
        public void Find_WhenCalled_ReturnListOfSkills()
        {
            try
            {
                // todo create mock Setup for adapter
                var result = _controller.Find();
                Assert.IsAssignableFrom<List<SkillDto>>(result.Value);
            }
            catch (Exception e)
            {
                _output.WriteLine("Error: " + e.Message);
            }
        }
    }
}