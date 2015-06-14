using System.Collections.Generic;

namespace JsonIndex.Tests.Collections
{
    public class JsonTextCollection
    {
        private readonly HashSet<string> items;

        public JsonTextCollection()
        {
            this.items = new HashSet<string>();
        }

        public void Add(JsonText text)
        {
            this.items.Add(text.GetValue());
        }

        public IEnumerable<string> Items
        {
            get { return this.items; }
        }
    }
}