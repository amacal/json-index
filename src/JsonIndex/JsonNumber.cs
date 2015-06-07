﻿namespace JsonIndex
{
    public class JsonNumber : JsonNode
    {
        private readonly Index index;
        private readonly int offset;

        public JsonNumber(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public void Accept(JsonVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
