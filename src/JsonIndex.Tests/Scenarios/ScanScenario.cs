using JsonIndex.Tests.Constraints;
using System.Collections.Generic;

namespace JsonIndex.Tests.Scenarios
{
    public static class ScanScenario
    {
        public static IEnumerable<JsonScenario> Array()
        {
            JsonInstance instance = new JsonInlineSource
            {
                Name = "array-in-the-middle",
                Data = @"value[""aa"",""bb""]+23"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint("aa", "bb")
            };
        }

        public static IEnumerable<JsonScenario> Object()
        {
            JsonInstance instance = new JsonInlineSource
            {
                Name = "object-in-the-middle",
                Data = @"value{""prop"":""aa"",""prop"":""bb""}+23"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint("aa", "bb")
            };
        }

        public static IEnumerable<JsonScenario> Mixed()
        {
            JsonInstance instance = new JsonInlineSource
            {
                Name = "mixed",
                Data = @"value{""prop""://""aa"",[""prop"",""bb""]}+23[123]"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint("prop", "bb")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNumberConstraint("123")
            };
        }
    }
}
