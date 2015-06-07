using System.Collections.Generic;
using System.Linq;

namespace JsonIndex
{
    public class JsonArray : JsonNode
    {
        private readonly Index index;
        private readonly int offset;

        public JsonArray(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public IEnumerable<JsonNode> GetItems()
        {
            return Enumerable.Empty<JsonNode>();
        }
    }
}
