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
            string data = scenario.Instance.GetData();
            Index index = IndexFactory.Build(data);

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
            string data = scenario.Instance.GetData();
            Index index = IndexFactory.Build(data);

            // act
            IEnumerable<JsonNode> nodes = index.Root.Flatten();

            // assert
            scenario.Constraint.Verify(nodes);
        }

        [Test]
        [TestCaseSource(typeof(ViolationScenario), "All")]
        public void IndexingShouldCauseAnException(JsonInstance instance)
        {
            // arrange
            string data = instance.GetData();

            // act
            TestDelegate act = () => IndexFactory.Build(data);

            // assert
            Assert.That(act, Throws.InstanceOf<IndexException>());
        }

        [Test]
        [TestCaseSource(typeof(ScanScenario), "Array")]
        [TestCaseSource(typeof(ScanScenario), "Object")]
        [TestCaseSource(typeof(ScanScenario), "Mixed")]
        public void ScanningShouldExtractTheStructure(JsonScenario scenario)
        {
            // arrange
            JsonCollector collector = new JsonCollector();
            string data = scenario.Instance.GetData();
            IEnumerable<Index> indices = IndexFactory.Scan(data);

            // act
            foreach (Index index in indices)
            {
                index.Root.Accept(collector);
            }

            // assert
            scenario.Constraint.Verify(collector);
        }
    }
}