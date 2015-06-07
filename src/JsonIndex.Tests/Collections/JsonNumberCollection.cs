﻿using System.Collections.Generic;

namespace JsonIndex.Tests.Collections
{
    public class JsonNumberCollection
    {
        private readonly HashSet<string> items;

        public JsonNumberCollection()
        {
            this.items = new HashSet<string>();
        }

        public void Add(JsonNumber text)
        {
            this.items.Add(text.GetValue());
        }

        public IEnumerable<string> Items
        {
            get { return this.items; }
        }
    }
}
