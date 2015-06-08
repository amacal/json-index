using System.Collections.Generic;
using System.Linq;

namespace JsonIndex.Tests.Collections
{
    public class JsonObjectCollection
    {
        public readonly List<JsonObject> items;

        public JsonObjectCollection()
        {
            this.items = new List<JsonObject>();
        }

        public string Get(string name)
        {
            foreach (JsonObject instance in this.items)
            {
                JsonProperty property = instance.Properties[name];
                if (property != null)
                {
                    return property.GetValue().ToString();
                }
            }

            return null;
        }

        public void Add(JsonObject instance)
        {
            this.items.Add(instance);
        }
    }
}
