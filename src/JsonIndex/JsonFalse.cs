using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonFalse : JsonNode
    {
        public static readonly JsonFalse Instance;

        static JsonFalse()
        {
            Instance = new JsonFalse();
        }

        private JsonFalse()
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
            return "false";
        }
    }
}