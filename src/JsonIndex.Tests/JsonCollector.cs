using System.Collections.Generic;
using JsonIndex.Tests.Collections;

namespace JsonIndex.Tests
{
    public class JsonCollector : JsonVisitor
    {
        private readonly JsonDataCollection data;
        private readonly JsonNameCollection names;
        private readonly JsonTextCollection texts;
        private readonly JsonNumberCollection numbers;
        private readonly JsonPrimitiveCollection primitives;

        public JsonCollector()
        {
            this.data = new JsonDataCollection();
            this.names = new JsonNameCollection();
            this.texts = new JsonTextCollection();
            this.numbers = new JsonNumberCollection();
            this.primitives = new JsonPrimitiveCollection();
        }

        public JsonDataCollection Data
        {
            get { return this.data; }
        }

        public JsonNameCollection Names
        {
            get { return this.names; }
        }

        public JsonTextCollection Texts
        {
            get { return this.texts; }
        }

        public JsonNumberCollection Numbers
        {
            get { return this.numbers; }
        }

        public JsonPrimitiveCollection Primitives
        {
            get { return this.primitives; }
        }

        public void Visit(JsonObject instance)
        {
            this.data.Add(instance);

            foreach (JsonProperty property in instance.GetProperties())
            {
                property.Accept(this);
            }
        }

        public void Visit(JsonProperty property)
        {
            this.names.Add(property);
            this.data.Add(property);

            property.GetValue().Accept(this);
        }

        public void Visit(JsonArray array)
        {
            this.data.Add(array);

            foreach (JsonNode node in array.GetItems())
            {
                node.Accept(this);
            }
        }

        public void Visit(JsonText text)
        {
            this.texts.Add(text);
        }

        public void Visit(JsonNumber number)
        {
            this.numbers.Add(number);
        }

        public void Visit(JsonTrue value)
        {
            this.primitives.Add(value);
        }

        public void Visit(JsonFalse value)
        {
            this.primitives.Add(value);
        }

        public void Visit(JsonNull value)
        {
            this.primitives.Add(value);
        }
    }
}
