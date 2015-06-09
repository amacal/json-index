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
                int child = this.offset;
                int count = 0;

                int hash = this.index.Hash(name);
                IndexEntry entry = this.index[child];

                while (child > 0)
                {
                    if (count % 2 == 0)
                    {
                        if (hash == this.index.Hash(child))
                        {
                            if (this.index.Equals(name, entry) == true)
                            {
                                return new JsonProperty(this.index, child);
                            }
                        }
                    }

                    child = entry.Next;
                    entry = this.index[child];

                    count++;
                }

                return null;
            }
        }

        public IEnumerator<JsonProperty> GetEnumerator()
        {
            int child = this.offset;
            int count = 0;

            while (child > 0)
            {
                if (count % 2 == 0)
                {
                    yield return new JsonProperty(this.index, child);
                }

                child = this.index[child].Next;
                count++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
