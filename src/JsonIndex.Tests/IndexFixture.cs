using JsonIndex.Tests.Scenarios;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace JsonIndex.Tests
{
    [TestFixture]
    public class IndexFixture
    {
        [Test]
        [TestCaseSource(typeof(ArrayScenario), "Empty")]
        [TestCaseSource(typeof(ArrayScenario), "Mixed")]
        [TestCaseSource(typeof(NumberScenario), "Default")]
        [TestCaseSource(typeof(ObjectScenario), "Empty")]
        [TestCaseSource(typeof(ObjectScenario), "Mixed")]
        [TestCaseSource(typeof(TextScenario), "Default")]
        [TestCaseSource(typeof(TextScenario), "Escape")]
        [TestCaseSource(typeof(TextScenario), "ByteOrderMark")]
        public void IndexingAndVisitingShouldExtractTheStructure(JsonScenario scenario)
        {
            // arrange
            JsonCollector collector = new JsonCollector();
            Index index = Index.Build(scenario.Instance.Data);

            // act
            index.Root.Accept(collector);

            // assert
            scenario.Constraint.Verify(collector);
        }

        [Test]
        [TestCaseSource(typeof(ArrayScenario), "Empty")]
        [TestCaseSource(typeof(ArrayScenario), "Mixed")]
        [TestCaseSource(typeof(NumberScenario), "Default")]
        [TestCaseSource(typeof(ObjectScenario), "Empty")]
        [TestCaseSource(typeof(ObjectScenario), "Mixed")]
        [TestCaseSource(typeof(TextScenario), "Default")]
        [TestCaseSource(typeof(TextScenario), "Escape")]
        [TestCaseSource(typeof(TextScenario), "ByteOrderMark")]
        public void IndexingAndEnumeratingShouldExtractTheStructure(JsonScenario scenario)
        {
            // arrange
            Index index = Index.Build(scenario.Instance.Data);

            // act
            IEnumerable<JsonNode> nodes = index.Root.Flatten();

            // assert
            scenario.Constraint.Verify(nodes);
        }

        [Test]
        [TestCaseSource(typeof(ViolationScenario), "All")]
        public void IndexingShouldCauseAnException(JsonInstance instance)
        {
            // act
            TestDelegate act = () => Index.Build(instance.Data);

            // assert
            Assert.That(act, Throws.InstanceOf<IndexException>());
        }
    }
}