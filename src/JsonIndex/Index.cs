using System.Collections.Generic;

namespace JsonIndex
{
    public class Index
    {
        private const int BucketSize = 10240;

        private readonly string data;
        private readonly List<IndexEntry[]> entries;
        private int total;

        internal Index(string data)
        {
            this.data = data;
            this.entries = new List<IndexEntry[]>();
        }

        public JsonObject Root
        {
            get { return new JsonObject(this, 0); }
        }

        internal string GetData(int index)
        {
            IndexEntry entry = this.entries[index / BucketSize][index % BucketSize];
            string data = this.data.Substring(entry.Start, entry.End - entry.Start + 1);

            return data;
        }

        internal IndexEntry this[int index]
        {
            get { return this.entries[index / BucketSize][index % BucketSize]; }
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
                Parent = parent,
                Start = start,
                End = end
            };

            if (parent >= 0)
            {
                IndexEntry owner = this.entries[parent / BucketSize][parent % BucketSize];

                if (owner.First == 0)
                {
                    owner.First = total;
                }

                if (owner.Last > 0)
                {
                    this.entries[owner.Last / BucketSize][owner.Last % BucketSize].Next = total;
                }

                owner.Last = total;
                this.entries[parent / BucketSize][parent % BucketSize] = owner;
            }

            if (current == 0)
            {
                this.entries.Add(new IndexEntry[BucketSize]);
            }

            this.entries[total / BucketSize][total % BucketSize] = entry;
            this.total = (total + 1) % BucketSize;

            return current;
        }

        public static Index Build(string data)
        {
            IndexBuilder builder = new IndexBuilder(data);
            Index index = builder.Build();

            return index;
        }
    }
}
