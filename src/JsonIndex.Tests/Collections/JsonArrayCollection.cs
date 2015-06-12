namespace JsonIndex.Tests.Collections
{
    public class JsonArrayCollection
    {
        private int count;

        public void Add(JsonItem item)
        {
            this.count++;
        }

        public int Count
        {
            get { return this.count; }
        }
    }
}
