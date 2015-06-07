using System.Collections.Generic;

namespace JsonIndex.Tests
{
    public static class JsonScenarioFactory
    {
        public static IEnumerable<JsonScenario> EmptyObject()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "empty-object",
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
                Constraint = new JsonDataConstraint()
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
        }

        public static IEnumerable<JsonScenario> EmptyArray()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "empty-array",
                Data = @"{""property"":[]}"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonDataConstraint("[]")
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
        }

        public static IEnumerable<JsonScenario> TextProperties()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "text-properties",
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
                Constraint = new JsonDataConstraint("value-a", "value-b")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonTextConstraint("value-a", "value-b")
            };
        }

        public static IEnumerable<JsonScenario> NumberProperties()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "text-properties",
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
                Constraint = new JsonDataConstraint("123", "123.456", "-123", "12.13e-7")
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonNumberConstraint("123", "123.456", "-123", "12.13e-7")
            };
        }

        public static IEnumerable<JsonScenario> MixedArray()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "mixed-array",
                Data = @"{""property"":[""abc"",123,true,false,null,{},[]]}"
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
        }
    }
}
