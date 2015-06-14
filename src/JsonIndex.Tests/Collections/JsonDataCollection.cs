using System.Collections.Generic;

namespace JsonIndex.Tests.Collections
{
    public class JsonDataCollection
    {
        private readonly HashSet<string> items;

        public JsonDataCollection()
        {
            this.items = new HashSet<string>();
        }

        public void Add(JsonObject instance)
        {
            this.items.Add(instance.ToString());
        }

        public void Add(JsonProperty property)
        {
            this.items.Add(property.ToString());
        }

        public void Add(JsonArray array)
        {
            this.items.Add(array.ToString());
        }

        public IEnumerable<string> Items
        {
            get { return this.items; }
        }
    }
}