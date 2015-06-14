using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonObject : JsonNode
    {
        private readonly Index index;
        private readonly int offset;

        public JsonObject(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public IEnumerable<JsonNode> GetChildren()
        {
            return this.Properties;
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public JsonPropertyCollection Properties
        {
            get { return new JsonPropertyCollection(this.index, this.index[this.offset].First); }
        }

        public override string ToString()
        {
            return this.index.GetData(this.offset);
        }
    }
}