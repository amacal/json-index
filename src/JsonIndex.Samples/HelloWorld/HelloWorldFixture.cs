using System.Collections.Generic;

using NUnit.Framework;

namespace JsonIndex.Samples.HelloWorld
{
    [TestFixture]
    public class HelloWorldFixture
    {
        [Test]
        [TestCaseSource("Cases")]
        public void ShouldReturnNumberOfOccurrencesOfHelloWorld(int expected, string data)
        {
            // arrange
            HelloWorldSample sample = new HelloWorldSample();

            // act
            int count = sample.Count(data);

            // assert
            Assert.That(count, Is.EqualTo(expected));
        }

        public static IEnumerable<TestCaseData> Cases()
        {
            yield return new TestCaseData(0, "{}");

            yield return new TestCaseData(1, @"

{
    ""property"" : ""hello-world""
}"
                );

            yield return new TestCaseData(1, @"

[
    ""I am too hello-world!"", ""I am not""
]"
                );
        }
    }
}
