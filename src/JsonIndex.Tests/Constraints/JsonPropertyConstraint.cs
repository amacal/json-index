using System;
using NUnit.Framework;

namespace JsonIndex.Tests.Constraints
{
    public class JsonPropertyConstraint : JsonConstraint
    {
        private readonly string name;
        private readonly string value;

        public JsonPropertyConstraint(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string Describe()
        {
            return String.Format("property: {0} = {1}", this.name, this.value);
        }

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Objects.Get(this.name), Is.EqualTo(this.value));
        }
    }
}
