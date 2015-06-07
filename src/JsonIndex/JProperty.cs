namespace JsonIndex
{
    public class JProperty
    {
        private readonly Index index;
        private readonly int offset;

        public JProperty(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public string GetName()
        {
            return this.index.GetData(this.offset);
        }

        public string GetValue()
        {
            return this.index.GetData(this.offset + 1);
        }
    }
}
