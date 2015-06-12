using System.Collections.Generic;

namespace JsonIndex.Tests
{
    public interface JsonConstraint
    {
        string Describe();

        void Verify(JsonCollector collector);

        void Verify(IEnumerable<JsonNode> nodes);
    }
}
