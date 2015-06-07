using System;
using NUnit.Framework;

namespace JsonIndex.Tests
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
            Assert.That(collector.Data, Is.EquivalentTo(this.data));
        }
    }
}
