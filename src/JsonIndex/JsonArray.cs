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
            IndexEntry entry = this.index[this.offset];
            int child = entry.First;
            int count = 0;

            while (child > 0)
            {
                yield return new JsonItem(this.index, child, count);
                child = this.index[child].Next;
                count++;
            }
        }

        public override string ToString()
        {
            return this.index.GetData(this.offset);
        }
    }
}
