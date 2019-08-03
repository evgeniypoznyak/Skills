using Skills.Domain.ValueObject;
using Xunit;

namespace Skills.Domain.Tests.ValueObject
{
    public class ProjectNameTests
    {
        [Fact]
        public void ProjectName_WhenCreated_SameStringAsExpectedInConstructor()
        {
            const string expected = "Test Name";
            var projectName = new ProjectName(expected);
            Assert.Equal(expected, projectName.Name);
        }

        [Theory,
         InlineData("", ""),
         InlineData(" ", ""),
         InlineData(" Test Name ", "Test Name"),
         InlineData("?*?*? ///Test Name?*?*?", "Test Name"),
         InlineData("?*?*? /// Test           Name ?*?*?", "Test Name")
        ]
        public void ProjectName_WhenCreated_EscapedAndSanitized(string testValue, string expected)
        {
            var projectName = new ProjectName(testValue);
            Assert.Equal(expected, projectName.Name);
        }
    }
}