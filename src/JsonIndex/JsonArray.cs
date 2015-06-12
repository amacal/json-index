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

        public IEnumerable<JsonNode> GetChildren()
        {
            return this.Items;
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public JsonItemCollection Items
        {
            get { return new JsonItemCollection(this.index, this.index[this.offset].First); }
        }

        public override string ToString()
        {
            return this.index.GetData(this.offset);
        }
    }
}
