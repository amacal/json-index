using System;

namespace JsonIndex.Tests
{
    public class JsonScenario
    {
        public JsonInstance Instance { get; set; }

        public JsonConstraint Constraint { get; set; }

        public override string ToString()
        {
            return String.Join(" | ", this.Instance.Name, this.Constraint.Describe());
        }
    }
}