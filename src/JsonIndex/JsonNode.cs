namespace JsonIndex
{
    public interface JsonNode
    {
        void Accept(JsonVisitor visitor);

        string ToString();
    }
}
