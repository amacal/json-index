namespace JsonIndex.Tests.Collections
{
    public class JsonPrimitiveCollection
    {
        private int nulls;
        private int trues;
        private int falses;

        public void Add(JsonNull value)
        {
            this.nulls++;
        }

        public void Add(JsonTrue value)
        {
            this.trues++;
        }

        public void Add(JsonFalse value)
        {
            this.falses++;
        }

        public int Null
        {
            get { return this.nulls; }
        }

        public int True
        {
            get { return this.trues; }
        }
        public int False
        {
            get { return this.falses; }
        }
    }
}
