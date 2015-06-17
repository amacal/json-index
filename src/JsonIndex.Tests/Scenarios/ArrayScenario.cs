﻿using JsonIndex.Tests.Constraints;
using System.Collections.Generic;

namespace JsonIndex.Tests.Scenarios
{
    public static class ArrayScenario
    {
        public static IEnumerable<JsonScenario> Empty()
        {
            JsonInstance instance = new JsonInlineSource
            {
                Name = "array-empty",
                Data = "[]"
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

            yield return new JsonScenario
            {
                Instance = instance,
                Constraint = new JsonArrayConstraint(0)
            };
        }

        public static IEnumerable<JsonScenario> Mixed()
        {
            JsonInstance instance = new JsonInlineSource
            {
                Name = "array-mixed",
                Data = @"[""abc"",123,true,false,null,{},[]]"
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
                Constraint = new JsonArrayConstraint(7)
            };
        }
    }
}