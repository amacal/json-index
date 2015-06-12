using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace JsonIndex.Tests.Constraints
{
    public class JsonDataConstraint : JsonConstraint
    {
        private readonly string[] data;

        public JsonDataConstraint(params string[] data)
        {
            this.data = data;
        }

        public string Describe()
        {
            return "data: " + String.Join(", ", this.data);
        }

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Data.Items, Is.EquivalentTo(this.data));
        }

        public void Verify(IEnumerable<JsonNode> nodes)
        {
            HashSet<string> data = new HashSet<string>();

            foreach (JsonNode node in nodes)
            {
                if (node is JsonObject || node is JsonProperty || node is JsonArray)
                {
                    data.Add(node.ToString());
                }
            }

            Assert.That(data, Is.EquivalentTo(this.data));
        }
    }
}
