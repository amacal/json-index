using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonItem : JsonNode
    {
        private readonly Index index;
        private readonly int offset;
        private readonly int order;

        public JsonItem(Index index, int offset, int order)
        {
            this.index = index;
            this.offset = offset;
            this.order = order;
        }

        public IEnumerable<JsonNode> GetChildren()
        {
            yield return this.GetValue();
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public int Index
        {
            get { return this.order; }
        }

        public JsonNode GetValue()
        {
            return JsonContainer.GetValue(this.index, this.offset);
        }
    }
}