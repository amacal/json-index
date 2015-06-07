namespace JsonIndex
{
    public interface JsonVisitor
    {
        void Visit(JsonObject instance);

        void Visit(JsonProperty property);

        void Visit(JsonArray array);

        void Visit(JsonText text);

        void Visit(JsonNumber number);

        void Visit(JsonTrue value);

        void Visit(JsonFalse value);

        void Visit(JsonNull value);
    }
}
