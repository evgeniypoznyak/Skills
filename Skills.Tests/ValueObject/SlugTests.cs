using Skills.ValueObject;
using Xunit;

namespace Skills.Tests.ValueObject
{
    public class SlugTests
    {
        [Fact]
        public void Slug_WhenCreated_SameStringAsExpectedInConstructor()
        {
            const string expected = "test-name";
            var slug = new Slug(expected);
            Assert.Equal(expected, slug.Name);
        }

        [Theory,
         InlineData("", ""),
         InlineData(" ", ""),
        //todo  InlineData("-Test-Name-", "test-name"),
         InlineData(" Test Name ", "test-name"),
         InlineData("?*?*? ///Test Name?*?*?", "test-name"),
         InlineData("?*?*? /// Test           Name ?*?*?", "test-name")
        ]
        public void Slug_WhenCreated_EscapedAndSanitized(string testValue, string expected)
        {
            var slug = new Slug(testValue);
            Assert.Equal(expected, slug.Name);
        }
    }
}