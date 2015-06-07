namespace JsonIndex
{
    public class JsonText : JsonNode
    {
        private readonly Index index;
        private readonly int offset;

        public JsonText(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public string GetValue()
        {
            return this.index.GetData(this.offset);
        }

        public override string ToString()
        {
            return this.index.GetData(this.offset);
        }
    }
}
