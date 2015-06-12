using System.Collections.Generic;
using JsonIndex.Tests.Collections;

namespace JsonIndex.Tests
{
    public class JsonCollector : JsonVisitorBase
    {
        private readonly JsonDataCollection data;
        private readonly JsonNameCollection names;
        private readonly JsonTextCollection texts;
        private readonly JsonItemCollection items;
        private readonly JsonObjectCollection objects;
        private readonly JsonNumberCollection numbers;
        private readonly JsonPrimitiveCollection primitives;

        public JsonCollector()
        {
            this.data = new JsonDataCollection();
            this.names = new JsonNameCollection();
            this.texts = new JsonTextCollection();
            this.items = new JsonItemCollection();
            this.objects = new JsonObjectCollection();
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

        public JsonItemCollection Items
        {
            get { return this.items; }
        }

        public JsonObjectCollection Objects
        {
            get { return this.objects; }
        }

        public JsonNumberCollection Numbers
        {
            get { return this.numbers; }
        }

        public JsonPrimitiveCollection Primitives
        {
            get { return this.primitives; }
        }

        public override void Visit(JsonObject instance)
        {
            this.data.Add(instance);
            this.objects.Add(instance);
            base.Visit(instance);
        }

        public override void Visit(JsonProperty property)
        {
            this.names.Add(property);
            this.data.Add(property);
            base.Visit(property);
        }

        public override void Visit(JsonArray array)
        {
            this.data.Add(array);
            base.Visit(array);
        }

        public override void Visit(JsonItem item)
        {
            this.items.Add(item);
            base.Visit(item);
        }

        public override void Visit(JsonText text)
        {
            this.texts.Add(text);
        }

        public override void Visit(JsonNumber number)
        {
            this.numbers.Add(number);
        }

        public override void Visit(JsonTrue value)
        {
            this.primitives.Add(value);
        }

        public override void Visit(JsonFalse value)
        {
            this.primitives.Add(value);
        }

        public override void Visit(JsonNull value)
        {
            this.primitives.Add(value);
        }
    }
}
