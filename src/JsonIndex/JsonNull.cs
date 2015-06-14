using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonNull : JsonNode
    {
        public IEnumerable<JsonNode> GetChildren()
        {
            yield break;
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "null";
        }
    }
}