namespace JsonIndex
{
    public class JsonPropertyName
    {
        private readonly Index index;
        private readonly int offset;

        public JsonPropertyName(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public string Value
        {
            get { return this.index.GetData(this.offset); }
        }

        public override int GetHashCode()
        {
            return this.index.Hash(this.offset);
        }

        public override bool Equals(object obj)
        {
            JsonPropertyName other = (JsonPropertyName)obj;

            return this.index.Equals(this.index[this.offset], this.index[other.offset]);
        }
    }
}
