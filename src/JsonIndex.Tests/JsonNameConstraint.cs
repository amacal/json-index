using System;
using NUnit.Framework;

namespace JsonIndex.Tests
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
            Assert.That(collector.Names, Is.EquivalentTo(this.names));
        }
    }
}
