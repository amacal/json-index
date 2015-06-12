using System.Collections.Generic;

namespace JsonIndex.Tests
{
    public static class JsonExtensions
    {
        public static IEnumerable<JsonNode> Flatten(this JsonNode node)
        {
            yield return node;

            foreach(JsonNode child in node.GetChildren())
            {
                foreach (JsonNode item in child.Flatten())
                {
                    yield return item;
                }
            }
        }
    }
}
