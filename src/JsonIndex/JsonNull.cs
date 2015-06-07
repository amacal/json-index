﻿namespace JsonIndex
{
    public class JsonNull : JsonNode
    {
        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "null";
        }
    }
}
