namespace JsonIndex
{
    public class IndexViolation
    {
        private readonly int position;
        private readonly string reason;

        public IndexViolation(int position, string reason)
        {
            this.position = position;
            this.reason = reason;
        }

        public int Position
        {
            get { return this.position; }
        }

        public string Reason
        {
            get { return this.reason; }
        }
    }
}
