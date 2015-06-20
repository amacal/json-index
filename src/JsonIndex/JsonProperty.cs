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

        public JsonPropertyName Name
        {
            get { return new JsonPropertyName(this.index, this.offset); }
        }

        public JsonNode GetValue()
        {
            IndexEntry entry = this.index[this.offset];
            return JsonContainer.GetValue(this.index, entry.Next);
        }

        public override string ToString()
        {
            return this.GetValue().ToString();
        }
    }
}