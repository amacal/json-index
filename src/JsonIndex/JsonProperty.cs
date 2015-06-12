using System;
using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonProperty : JsonNode
    {
        private readonly Index index;
        private readonly int offset;

        public JsonProperty(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public IEnumerable<JsonNode> GetChildren()
        {
            yield return this.GetValue();
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetName()
        {
            return this.index.GetData(this.offset);
        }

        public JsonNode GetValue()
        {
            return JsonContainer.GetValue(this.index, this.offset + 1);
        }

        public override string ToString()
        {
            return this.index.GetData(this.offset + 1);
        }
    }
}
