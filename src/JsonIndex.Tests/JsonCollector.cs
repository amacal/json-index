using System.Collections.Generic;

namespace JsonIndex.Tests
{
    public class JsonCollector : JsonVisitor
    {
        private readonly List<string> data;
        private readonly List<string> names;
        private readonly List<string> texts;
        private readonly List<string> numbers;
        private readonly JsonPrimitiveCollection primitives;

        public JsonCollector()
        {
            this.data = new List<string>();
            this.names = new List<string>();
            this.texts = new List<string>();
            this.numbers = new List<string>();
            this.primitives = new JsonPrimitiveCollection();
        }

        public IEnumerable<string> Data
        {
            get { return this.data; }
        }

        public IEnumerable<string> Names
        {
            get { return this.names; }
        }

        public IEnumerable<string> Texts
        {
            get { return this.texts; }
        }

        public IEnumerable<string> Numbers
        {
            get { return this.numbers; }
        }

        public JsonPrimitiveCollection Primitives
        {
            get { return this.primitives; }
        }

        public void Visit(JsonObject instance)
        {
            foreach (JsonProperty property in instance.GetProperties())
            {
                property.Accept(this);
            }
        }

        public void Visit(JsonProperty property)
        {
            this.names.Add(property.GetName());
            this.data.Add(property.GetText());

            property.GetValue().Accept(this);
        }

        public void Visit(JsonArray array)
        {
            foreach (JsonNode node in array.GetItems())
            {
                node.Accept(this);
            }
        }

        public void Visit(JsonText text)
        {
            this.texts.Add(text.GetValue());
        }

        public void Visit(JsonNumber number)
        {
            this.numbers.Add(number.GetValue());
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
