using System;

namespace JsonIndex
{   
    public class IndexException : Exception
    {
        private readonly IndexViolation[] violations;

        public IndexException(string message, IndexViolation[] violations)
            : base(message)
        {
            this.violations = violations;
        }

        public IndexViolation[] Violations
        {
            get { return this.violations; }
        }
    }
}