using NUnit.Framework;
namespace JsonIndex.Tests.Constraints
{
    public class JsonArrayConstraint : JsonConstraint
    {
        private readonly int count;

        public JsonArrayConstraint(int count)
        {
            this.count = count;
        }

        public string Describe()
        {
            return "array: " + this.count;
        }

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Items.Count, Is.EqualTo(this.count));
        }
    }
}
