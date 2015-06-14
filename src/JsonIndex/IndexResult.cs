namespace JsonIndex
{
    internal class IndexResult
    {
        private readonly Index index;
        private readonly IndexViolation[] violations;

        public IndexResult(Index index, IndexViolation[] violations)
        {
            this.index = index;
            this.violations = violations;
        }

        public Index Index
        {
            get { return this.index; }
        }

        public IndexViolation[] Violations
        {
            get { return this.violations; }
        }

        public bool IsSuccessful()
        {
            return this.violations.Length == 0;
        }
    }
}
