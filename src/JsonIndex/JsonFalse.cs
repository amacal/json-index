namespace JsonIndex
{
    public class JsonFalse : JsonNode
    {
        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "false";
        }
    }
}
