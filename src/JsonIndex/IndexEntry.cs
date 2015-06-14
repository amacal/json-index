namespace JsonIndex
{
    internal struct IndexEntry
    {
        public byte Type;

        public int First;
        public int Next;
        public int Last;

        public int Hash;
        public int Start;
        public int End;
    }
}