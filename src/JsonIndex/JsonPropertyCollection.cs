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

                int hash = this.Hash(name);
                IndexEntry entry = this.index[child];

                while (child > 0)
                {
                    if (count % 2 == 0)
                    {
                        if (hash == this.Hash(entry))
                        {
                            if (this.Equals(name, entry) == true)
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

        private int Hash(string name)
        {
            int hash = 0;

            for (int i = 0; i < name.Length; i++)
            {
                hash = 31 * hash + name[i];
            }

            return hash;
        }

        private int Hash(IndexEntry entry)
        {
            int hash = 0;

            for (int i = entry.Start; i <= entry.End; i++)
            {
                hash = 31 * hash + this.index.Data[i];
            }

            return hash;
        }

        private bool Equals(string name, IndexEntry entry)
        {
            if (entry.End - entry.Start != name.Length - 1)
            {
                return false;
            }

            for (int i = 0, j = entry.Start; i < name.Length; i++, j++)
            {
                if (name[i] != this.index.Data[j])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
