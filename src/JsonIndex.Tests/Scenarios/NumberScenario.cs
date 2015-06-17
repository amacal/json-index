using JsonIndex.Tests.Constraints;
using System.Collections.Generic;

namespace JsonIndex.Tests.Scenarios
{
    public static class NumberScenario
    {
        public static IEnumerable<JsonScenario> Default()
        {
            JsonInstance instance = new JsonInlineSource
            {
                Name = "number-default",
                Data = @"{""prop-a"":123,""prop-b"":123.456,""prop-c"":-123,""prop-d"":12.13e-7}"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNameConstraint("prop-a", "prop-b", "prop-c", "prop-d")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonDataConstraint("123", "123.456", "-123", "12.13e-7", @"{""prop-a"":123,""prop-b"":123.456,""prop-c"":-123,""prop-d"":12.13e-7}")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNumberConstraint("123", "123.456", "-123", "12.13e-7")
            };
        }
    }
}