using System.Collections;
using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonPropertyCollection : IEnumerable<JsonProperty>
    {
        private readonly Index index;
        private readonly int offset;

        public JsonPropertyCollection(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public JsonProperty this[string name]
        {
            get
            {
                int hash = this.index.Hash(name);
                int previous = this.offset, child = this.offset;
                IndexEntry entry;

                while (child > 0)
                {
                    entry = this.index[child];

                    if (entry.Type != IndexType.Property)
                    {
                        if (hash == this.index.Hash(previous))
                        {
                            if (this.index.Equals(name, this.index[previous]) == true)
                            {
                                return new JsonProperty(this.index, previous);
                            }
                        }
                    }

                    previous = child;
                    child = entry.Next;
                }

                return null;
            }
        }

        public IEnumerator<JsonProperty> GetEnumerator()
        {
            int child = this.offset;
            IndexEntry entry;

            while (child > 0)
            {
                entry = this.index[child];

                if (entry.Type == IndexType.Property)
                {
                    yield return new JsonProperty(this.index, child);
                }

                child = entry.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}