using System;
using NUnit.Framework;

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
    }
}
