using System.Linq;
using NUnit.Framework;

namespace JsonIndex
{
    public class IndexFixture
    {
        [TestFixture]
        public class EmptyObject
        {
            public Index Index;

            [SetUp]
            public void Initialize()
            {
                this.Index = Index.Build("{}");
            }

            [Test]
            public void ReturnsNotNull()
            {
                Assert.That(this.Index, Is.Not.Null);
            }

            [Test]
            public void ContainsRoot()
            {
                Assert.That(this.Index.Root, Is.Not.Null);
            }

            [Test]
            public void ContainsRootHavingNoProperties()
            {
                Assert.That(this.Index.Root.Properties(), Is.Empty);
            }
        }

        [TestFixture]
        public class SingleTextPropertyObject
        {
            public Index Index;

            [SetUp]
            public void Initialize()
            {
                this.Index = Index.Build(@"{""property"":""value""}");
            }

            [Test]
            public void ReturnsNotNull()
            {
                Assert.That(this.Index, Is.Not.Null);
            }

            [Test]
            public void ContainsRoot()
            {
                Assert.That(this.Index.Root, Is.Not.Null);
            }

            [Test]
            public void ContainsRootHavingOneProperty()
            {
                Assert.That(this.Index.Root.Properties().Count(), Is.EqualTo(1));
            }
        }
    }
}
