using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonIndex.Tests.Constraints
{
    public class JsonNameConstraint : JsonConstraint
    {
        private readonly string[] names;

        public JsonNameConstraint(params string[] names)
        {
            this.names = names;
        }

        public string Describe()
        {
            return "names: " + String.Join(", ", this.names);
        }

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Names.Items, Is.EquivalentTo(this.names));
        }

        public void Verify(IEnumerable<JsonNode> nodes)
        {
            Assert.That(nodes.OfType<JsonProperty>().Select(node => node.Name.Value), Is.EquivalentTo(this.names));
        }
    }
}