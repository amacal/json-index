using System.Collections.Generic;
using System.Linq;

namespace JsonIndex.Tests.Collections
{
    public class JsonNameCollection
    {
        private readonly HashSet<JsonPropertyName> items;

        public JsonNameCollection()
        {
            this.items = new HashSet<JsonPropertyName>();
        }

        public void Add(JsonProperty property)
        {
            this.items.Add(property.Name);
        }

        public IEnumerable<string> Items
        {
            get { return this.items.Select(x => x.Value); }
        }
    }
}