﻿using System;
using System.Collections.Generic;

namespace JsonIndex
{
    public class Index
    {
        private readonly string data;
        private readonly IndexSettings settings;
        private readonly List<IndexEntry[]> entries;
        private int total;

        internal Index(string data, IndexSettings settings)
        {
            this.data = data;
            this.settings = settings;
            this.entries = new List<IndexEntry[]>();
        }

        internal string Data
        {
            get { return this.data; }
        }

        internal IndexSettings Settings
        {
            get { return this.settings; }
        }

        public JsonNode Root
        {
            get
            {
                switch (this[0].Type)
                {
                    case IndexType.Object:
                        return new JsonObject(this, 0);

                    case IndexType.Array:
                        return new JsonArray(this, 0);

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        internal string GetData(int index)
        {
            IndexEntry entry = this[index];
            string data = this.data.Substring(entry.Start, entry.End - entry.Start + 1);

            return data;
        }

        internal IndexEntry this[int index]
        {
            get { return this.entries[index / this.settings.BucketSize][index % this.settings.BucketSize]; }
            private set { this.entries[index / this.settings.BucketSize][index % this.settings.BucketSize] = value; }
        }

        internal void End(int index, int end)
        {
            this.entries[index / this.settings.BucketSize][index % this.settings.BucketSize].End = end;
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
                    this.entries[owner.Last / this.settings.BucketSize][owner.Last % this.settings.BucketSize].Next = total;
                }

                owner.Last = total;
                this[parent] = owner;
            }

            if (current % this.settings.BucketSize == 0)
            {
                this.entries.Add(new IndexEntry[this.settings.BucketSize]);
            }

            this[total] = entry;
            this.total = total + 1;

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

        internal bool Equals(IndexEntry left, IndexEntry right)
        {
            if (left.End - left.Start != right.End - right.Start)
            {
                return false;
            }

            for (int i = left.Start, j = right.Start; i <= left.End; i++, j++)
            {
                if (this.data[i] != this.data[j])
                {
                    return false;
                }
            }

            return true;
        }
    }
}