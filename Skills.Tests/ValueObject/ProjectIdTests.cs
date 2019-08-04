using System;
using System.Collections.Generic;
using Skills.ValueObject;
using Xunit;

namespace Skills.Tests.ValueObject
{
    public class ProjectIdTests
    {
         [Fact]
        public void ProjectId_WhenCreated_SameTypeWithReportId()
        {
            var projectId = new ProjectId(Guid.NewGuid());
            Assert.IsType<ProjectId>(projectId);
        }

        [Fact]
        public void ProjectId_WhenCreated_GuidType()
        {
            var projectId = new ProjectId(Guid.NewGuid());
            Assert.IsType<Guid>(projectId.Id);
        }

        [Fact]
        public void ProjectId_WhenIdValueSet_ReturnGuidValue()
        {
            var expected = Guid.NewGuid();
            var projectId = new ProjectId(expected);
            Assert.Equal(expected, projectId.Id);
        }
        
        [Fact]
        public void FromString_IsCalled_ReturnNewProjectIdWithGuidType()
        {
            var guidString = Guid.NewGuid().ToString();
            var expected = Guid.Parse(guidString);
            var projectId = new ProjectId(guidString);
            Assert.Equal(expected, projectId.Id);
        }
        
        /// <summary>
        /// Properties returning IEnumerable can be used instead of InLineData.
        /// </summary>
        public static IEnumerable<object[]> SamplePropertyDataProperty
        {
            get
            {
                yield return new object[] { "" };
                yield return new object[] { "-" };
                yield return new object[] { "0" };
                yield return new object[] { "11111" };
                yield return new object[] { "sdfsdfAAA-" };
                yield return new object[] { "{key: value}" };
                yield return new object[] { "11238972347863287623478324763276423764623784623784678236478273842872364" };
            }
        }
        
        /// <summary>
        /// PropertyData allow multiple calls of a test function with parameters defined in a property usign yield
        /// </summary>
        /// <param name="value">Test data</param>
        [Theory]
        [MemberData(nameof(SamplePropertyDataProperty))]
        public void FromString_IsCalledWithBadString_ThrowsFormatException(string value)
        {
            Assert.Throws<FormatException>(() => new ProjectId(value));
        }
    }
}