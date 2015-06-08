using System.Collections.Generic;
using JsonIndex.Tests.Constraints;

namespace JsonIndex.Tests.Scenarios
{
    public static class ObjectScenario
    {
        public static IEnumerable<JsonScenario> Empty()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "object-empty",
                Data = "{}"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNameConstraint()
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonDataConstraint("{}")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint()
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNumberConstraint()
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNullConstraint()
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("number", null)
            };
        }

        public static IEnumerable<JsonScenario> Mixed()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "object-mixed",
                Data = @"{""text"":""abc"",""number"":123,""prop-true"":true,""prop-false"":false,""prop-null"":null,""object"":{},""array"":[]}"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint("abc")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNumberConstraint("123")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNullConstraint(1)
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonBooleanConstraint(false, 1)
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonBooleanConstraint(true, 1)
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonArrayConstraint(0)
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("text", "abc")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("number", "123")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("prop-true", "true")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("prop-false", "false")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("prop-null", "null")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("object", "{}")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonPropertyConstraint("array", "[]")
            };
        }
    }
}
