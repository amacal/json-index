using System;

namespace JsonIndex
{
    public class Index
    {
        private readonly string data;
        private readonly IndexEntry[] entries;
        private int total;

        public Index(string data)
        {
            this.data = data;
            this.entries = new IndexEntry[1024];
        }

        public JsonObject Root
        {
            get { return new JsonObject(this, 0); }
        }

        public string GetData(int index)
        {
            IndexEntry entry = this.entries[index];
            string data = this.data.Substring(entry.Start, entry.End - entry.Start + 1);

            return data;
        }

        public IndexEntry this[int index]
        {
            get { return this.entries[index]; }
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
