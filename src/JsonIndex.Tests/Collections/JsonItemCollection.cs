namespace JsonIndex.Tests.Collections
{
    public class JsonItemCollection
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
