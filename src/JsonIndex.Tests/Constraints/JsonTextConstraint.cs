using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JsonIndex.Tests.Constraints
{
    public class JsonTextConstraint : JsonConstraint
    {
        private readonly string[] texts;

        public JsonTextConstraint(params string[] texts)
        {
            this.texts = texts;
        }

        public string Describe()
        {
            return "texts: " + String.Join(", ", this.texts);
        }

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Texts.Items, Is.EquivalentTo(this.texts));
        }

        public void Verify(IEnumerable<JsonNode> nodes)
        {
            Assert.That(nodes.OfType<JsonText>().Select(node => node.GetValue()), Is.EquivalentTo(this.texts));
        }
    }
}