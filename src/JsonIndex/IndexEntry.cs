namespace JsonIndex
{
    public struct IndexEntry
    {
        public byte Type;

        public int Parent;
        public int First;
        public int Last;
        public int Next;

        public int Start;
        public int End;
    }
}
