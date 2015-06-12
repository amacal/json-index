using System.Collections.Generic;

namespace JsonIndex
{
    public interface JsonNode
    {
        IEnumerable<JsonNode> GetChildren();

        void Accept(JsonVisitor visitor);

        string ToString();
    }
}
