using System.Collections.Generic;

namespace JsonIndex
{
    public class Index
    {
        private const int BucketSize = 16384;

        private readonly string data;
        private readonly List<IndexEntry[]> entries;
        private int total;

        internal Index(string data)
        {
            this.data = data;
            this.entries = new List<IndexEntry[]>();
        }

        internal string Data
        {
            get { return this.data; }
        }

        public JsonObject Root
        {
            get { return new JsonObject(this, 0); }
        }

        internal string GetData(int index)
        {
            IndexEntry entry = this[index];
            string data = this.data.Substring(entry.Start, entry.End - entry.Start + 1);

            return data;
        }

        internal IndexEntry this[int index]
        {
            get { return this.entries[index / BucketSize][index % BucketSize]; }
            private set { this.entries[index / BucketSize][index % BucketSize] = value; }
        }

        internal void End(int index, int end)
        {
            this.entries[index / BucketSize][index % BucketSize].End = end;
        }

        internal int New(byte type, int parent, int start, int end)
        {
            int current = total;
            IndexEntry entry = new IndexEntry
            {
                Type = type,
                Start = start,
                End = end
            };

            if (parent >= 0)
            {
                IndexEntry owner = this[parent];

                if (owner.First == 0)
                {
                    owner.First = total;
                }

                if (owner.Last > 0)
                {
                    this.entries[owner.Last / BucketSize][owner.Last % BucketSize].Next = total;
                }

                owner.Last = total;
                this[parent] = owner;
            }

            if (current == 0)
            {
                this.entries.Add(new IndexEntry[BucketSize]);
            }

            this[total] = entry;
            this.total = (total + 1) % BucketSize;

            return current;
        }

        internal int Hash(string name)
        {
            int hash = 0;

            for (int i = 0; i < name.Length; i++)
            {
                hash = 31 * hash + name[i];
            }

            return hash;
        }

        internal int Hash(int index)
        {
            IndexEntry entry = this[index];
            int hash = entry.Hash;

            if (hash == 0)
            {
                for (int i = entry.Start; i <= entry.End; i++)
                {
                    hash = 31 * hash + this.data[i];
                }

                entry.Hash = hash;
                this[index] = entry;
            }

            return hash;
        }

        internal bool Equals(string name, IndexEntry entry)
        {
            if (entry.End - entry.Start != name.Length - 1)
            {
                return false;
            }

            for (int i = 0, j = entry.Start; i < name.Length; i++, j++)
            {
                if (name[i] != this.data[j])
                {
                    return false;
                }
            }

            return true;
        }

        public static Index Build(string data)
        {
            IndexBuilder builder = new IndexBuilder(data);
            Index index = builder.Build();

            return index;
        }
    }
}
