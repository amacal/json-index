namespace JsonIndex
{
    public class JsonTrue : JsonNode
    {
        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "true";
        }
    }
}
