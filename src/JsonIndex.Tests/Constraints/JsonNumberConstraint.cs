using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonIndex.Tests.Constraints
{
    public class JsonNumberConstraint : JsonConstraint
    {
        private readonly string[] numbers;

        public JsonNumberConstraint(params string[] numbers)
        {
            this.numbers = numbers;
        }

        public string Describe()
        {
            return "numbers: " + String.Join(", ", this.numbers);
        }

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Numbers.Items, Is.EquivalentTo(this.numbers));
        }

        public void Verify(IEnumerable<JsonNode> nodes)
        {
            Assert.That(nodes.OfType<JsonNumber>().Select(node => node.GetValue()), Is.EquivalentTo(this.numbers));
        }
    }
}