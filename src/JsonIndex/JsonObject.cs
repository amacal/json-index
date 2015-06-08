using System.Collections.Generic;
using System.Linq;

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

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public JsonPropertyCollection Properties
        {
            get
            {
                IndexEntry entry = this.index[this.offset];

                return new JsonPropertyCollection(this.index, entry.First);
            }
        }

        public override string ToString()
        {
            return this.index.GetData(this.offset);
        }
    }
}
