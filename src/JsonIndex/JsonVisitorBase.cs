namespace JsonIndex
{
    public abstract class JsonVisitorBase : JsonVisitor
    {
        public virtual void Visit(JsonObject instance)
        {
            foreach (JsonProperty property in instance.Properties)
            {
                property.Accept(this);
            }
        }

        public virtual void Visit(JsonProperty property)
        {
            property.GetValue().Accept(this);
        }

        public virtual void Visit(JsonArray array)
        {
            foreach (JsonItem item in array.GetItems())
            {
                item.Accept(this);
            }
        }

        public virtual void Visit(JsonItem item)
        {
            item.GetValue().Accept(this);
        }

        public virtual void Visit(JsonText text)
        {
        }

        public virtual void Visit(JsonNumber number)
        {
        }

        public virtual void Visit(JsonTrue value)
        {
        }

        public virtual void Visit(JsonFalse value)
        {
        }

        public virtual void Visit(JsonNull value)
        {
        }
    }
}
