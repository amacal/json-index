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
            public void ContainsNoProperties()
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
                this.Index = Index.Build(@"{""property"":""abc""}");
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
            public void ContainsOneProperty()
            {
                Assert.That(this.Index.Root.Properties().Count(), Is.EqualTo(1));
            }

            [Test]
            public void ContainsPropertyWithNameProperty()
            {
                Assert.That(this.Index.Root.Properties(), Has.All.Matches<JProperty>(x => x.GetName() == "property"));
            }

            [Test]
            public void ContainsPropertyWithValueAbc()
            {
                Assert.That(this.Index.Root.Properties(), Has.All.Matches<JProperty>(x => x.GetValue() == "abc"));
            }
        }

        [TestFixture]
        public class SingleNumberPropertyObject
        {
            public Index Index;

            [SetUp]
            public void Initialize()
            {
                this.Index = Index.Build(@"{""property"":123.456}");
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
            public void ContainsOneProperty()
            {
                Assert.That(this.Index.Root.Properties().Count(), Is.EqualTo(1));
            }

            [Test]
            public void ContainsPropertyWithNameProperty()
            {
                Assert.That(this.Index.Root.Properties(), Has.All.Matches<JProperty>(x => x.GetName() == "property"));
            }

            [Test]
            public void ContainsPropertyWithValue123456()
            {
                Assert.That(this.Index.Root.Properties(), Has.All.Matches<JProperty>(x => x.GetValue() == "123.456"));
            }
        }
    }
}
