using Skills.Domain.ValueObject;
using Xunit;

namespace Skills.Domain.Tests.ValueObject
{
    public class ObjectNameTests
    {
        [Fact]
        public void SkillName_WhenCreated_SameStringAsExpectedInConstructor()
        {
            const string expected = "Test Name";
            var skillName = new ObjectName(expected);
            Assert.Equal(expected, skillName.Name);
        }

        [Theory,
         InlineData("", ""),
         InlineData(" ", ""),
         InlineData(" Test Name ", "Test Name"),
         InlineData("?*?*? ///Test Name?*?*?", "Test Name"),
         InlineData("?*?*? /// Test           Name ?*?*?", "Test Name")
        ]
        public void SkillName_WhenCreated_EscapedAndSanitized(string testValue, string expected)
        {
            var skillName = new ObjectName(testValue);
            Assert.Equal(expected, skillName.Name);
        }
    }
}