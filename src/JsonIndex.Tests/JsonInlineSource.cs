namespace JsonIndex.Tests
{
    public class JsonInlineSource : JsonInstance
    {
        public string Name { get; set; }

        public string Data { get; set; }

        public string GetData()
        {
            return this.Data;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
