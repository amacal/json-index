using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonNull : JsonNode
    {
        public static readonly JsonNull Instance;

        static JsonNull()
        {
            Instance = new JsonNull();
        }

        private JsonNull()
        {
        }

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