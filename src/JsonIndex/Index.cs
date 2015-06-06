using System;

namespace JsonIndex
{
    public class Index
    {
        private readonly IndexEntry[] entries;
        private int total;

        public Index()
        {
            this.entries = new IndexEntry[1024];
        }

        public JObject Root
        {
            get { return new JObject(this, 0); }
        }

        public IndexEntry this[int index]
        {
            get { return this.entries[index]; }
        }

        public void Add(int parent, int child)
        {
            IndexEntry entry = this.entries[parent];

        }

        public void End(int index, int end)
        {
            this.entries[index].End = end;
        }

        public int New(byte type, int parent, int start, int end)
        {
            IndexEntry entry = new IndexEntry();

            entry.Type = type;
            entry.Parent = parent;
            entry.Start = start;
            entry.End = end;

            if (parent >= 0)
            {
                IndexEntry owner = this.entries[parent];

                if (owner.First == 0)
                {
                    owner.First = total;
                }

                if (owner.Last > 0)
                {
                    this.entries[owner.Last].Next = total;
                }

                owner.Last = total;
                this.entries[parent] = owner;
            }

            this.entries[total] = entry;
            return total++;
        }

        public static Index Build(string data)
        {
            IndexBuilder builder = new IndexBuilder(data);
            Index index = builder.Build();

            return index;
        }
    }
}
