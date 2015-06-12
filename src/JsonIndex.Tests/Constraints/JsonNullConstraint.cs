using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace JsonIndex.Tests.Constraints
{
    public class JsonNullConstraint : JsonConstraint
    {
        private readonly int count;

        public JsonNullConstraint()
        {
        }

        public JsonNullConstraint(int count)
        {
            this.count = count;
        }

        public string Describe()
        {
            return "nulls: " + this.count;
        }

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Primitives.Null, Is.EqualTo(this.count));
        }

        public void Verify(IEnumerable<JsonNode> nodes)
        {
            Assert.That(nodes.OfType<JsonNull>().Count(), Is.EqualTo(this.count));
        }
    }
}
