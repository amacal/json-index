using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace JsonIndex.Tests
{
    [TestFixture]
    public class IndexFixture
    {
        [Test]
        [TestCaseSource(typeof(JsonScenarioFactory), "EmptyObject")]
        [TestCaseSource(typeof(JsonScenarioFactory), "EmptyArray")]
        [TestCaseSource(typeof(JsonScenarioFactory), "TextProperties")]
        [TestCaseSource(typeof(JsonScenarioFactory), "NumberProperties")]
        [TestCaseSource(typeof(JsonScenarioFactory), "MixedArray")]
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
