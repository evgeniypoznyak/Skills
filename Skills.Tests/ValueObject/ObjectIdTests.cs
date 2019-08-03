using System;
using System.Collections.Generic;
using Skills.Domain.ValueObject;
using Xunit;
using Xunit.Abstractions;

namespace Skills.Domain.Tests.ValueObject
{
    public class ObjectIdTests
    {
        private readonly ITestOutputHelper _output;

        public ObjectIdTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void SkillId_WhenCreated_SameTypeWithReportId()
        {
            var skillId = new ObjectId(Guid.NewGuid());
            Assert.IsType<ObjectId>(skillId);
        }

        [Fact]
        public void SkillId_WhenCreated_GuidType()
        {
            var skillId = new ObjectId(Guid.NewGuid());
            Assert.IsType<Guid>(skillId.Id);
        }

        [Fact]
        public void SkillId_WhenIdValueSet_ReturnGuidValue()
        {
            var expected = Guid.NewGuid();
            var skillId = new ObjectId(expected);
            Assert.Equal(expected, skillId.Id);
        }

        [Fact]
        public void FromString_IsCalled_ReturnNewSkillIdWithGuidType()
        {
            var guidString = Guid.NewGuid().ToString();
            var expected = Guid.Parse(guidString);
            var objectId = new ObjectId(guidString);
            Assert.Equal(expected, objectId.Id);
        }

        /// <summary>
        /// Properties returning IEnumerable can be used instead of InLineData.
        /// </summary>
        public static IEnumerable<object[]> SamplePropertyDataProperty
        {
            get
            {
                yield return new object[] {""};
                yield return new object[] {"-"};
                yield return new object[] {"0"};
                yield return new object[] {"11111"};
                yield return new object[] {"sdfsdfAAA-"};
                yield return new object[] {"{key: value}"};
                yield return new object[] {"11238972347863287623478324763276423764623784623784678236478273842872364"};
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
            Assert.Throws<FormatException>(() => new ObjectId(value));
        }

        [Fact]
        public void ToString_IfCalled_ShouldBeSameAsPassedStringGuid()
        {
            var expected = Guid.NewGuid().ToString();
            var actual = new ObjectId(expected).ToString();
            Assert.Equal(expected, actual);
        }
    }
}