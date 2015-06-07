using System.Collections.Generic;

namespace JsonIndex.Tests.Collections
{
    public class JsonNameCollection
    {
        private readonly HashSet<string> items;

        public JsonNameCollection()
        {
            this.items = new HashSet<string>();
        }

        public void Add(JsonProperty property)
        {
            this.items.Add(property.GetName());
        }

        public IEnumerable<string> Items
        {
            get { return this.items; }
        }
    }
}
