namespace JsonIndex.Tests
{
    public class JsonInstance
    {
        public string Name { get; set; }

        public string Data { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}