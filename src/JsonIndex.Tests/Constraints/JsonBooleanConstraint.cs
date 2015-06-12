using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace JsonIndex.Tests.Constraints
{
    public class JsonBooleanConstraint : JsonConstraint
    {
        private readonly bool? value;
        private readonly int count;

        public JsonBooleanConstraint()
        {
        }

        public JsonBooleanConstraint(bool value, int count)
        {
            this.value = value;
            this.count = count;
        }

        public string Describe()
        {
            if (this.value == true)
            {
                return String.Format("bool: {0} (true)", this.count);
            }

            if (this.value == false)
            {
                return String.Format("bool: {0} (false)", this.count);
            }

            return "bool: nothing";
        }

        public void Verify(JsonCollector collector)
        {
            if (this.value == true)
            {
                Assert.That(collector.Primitives.True, Is.EqualTo(this.count));
            }

            if (this.value == false)
            {
                Assert.That(collector.Primitives.False, Is.EqualTo(this.count));
            }

            if (this.value == null)
            {
                Assert.That(collector.Primitives.True + collector.Primitives.False, Is.EqualTo(0));
            }
        }

        public void Verify(IEnumerable<JsonNode> nodes)
        {
            if (this.value == true)
            {
                Assert.That(nodes.OfType<JsonTrue>().Count(), Is.EqualTo(this.count));
            }

            if (this.value == false)
            {
                Assert.That(nodes.OfType<JsonFalse>().Count(), Is.EqualTo(this.count));
            }

            if (this.value == null)
            {
                Assert.That(nodes.OfType<JsonTrue>().Count() + nodes.OfType<JsonFalse>().Count(), Is.EqualTo(0));
            }
        }
    }
}
