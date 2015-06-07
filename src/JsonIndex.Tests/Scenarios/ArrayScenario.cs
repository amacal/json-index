using System.Collections.Generic;
using JsonIndex.Tests.Constraints;

namespace JsonIndex.Tests.Scenarios
{
    public static class ArrayScenario
    {
        public static IEnumerable<JsonScenario> Empty()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "array-empty",
                Data = @"{""property"":[]}"
            };

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonDataConstraint(@"{""property"":[]}", "[]")
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
                Constraint = new JsonBooleanConstraint()
            };
        }

        public static IEnumerable<JsonScenario> Mixed()
        {
            JsonInstance instance = new JsonInstance
            {
                Name = "array-mixed",
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
        }
    }
}
