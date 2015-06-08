using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using JsonIndex.Tests.Scenarios;

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
    }
}
