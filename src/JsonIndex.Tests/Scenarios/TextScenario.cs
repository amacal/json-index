using System.Collections.Generic;
using JsonIndex.Tests.Constraints;

namespace JsonIndex.Tests.Scenarios
{
    public static class TextScenario
    {
        public static IEnumerable<JsonScenario> Default()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "text-default",
                Data = @"{""prop-a"":""value-a"",""prop-b"":""value-b""}"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNameConstraint("prop-a", "prop-b")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonDataConstraint("value-a", "value-b", @"{""prop-a"":""value-a"",""prop-b"":""value-b""}")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint("value-a", "value-b")
            };
        }

        public static IEnumerable<JsonScenario> Escape()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "text-escape",
                Data = @"{""prop-a"":""\"""",""\\"":""\uabcdefgh"",""prop-c"":""""}"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint("\\\"", "\\uabcdefgh", "")
            };
        }
    }
}
