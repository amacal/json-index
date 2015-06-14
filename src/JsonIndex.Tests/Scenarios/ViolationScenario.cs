using System.Collections.Generic;

namespace JsonIndex.Tests.Scenarios
{
    public static class ViolationScenario
    {
        public static IEnumerable<JsonInstance> All()
        {
            yield return new JsonInstance
            {
                Name = "root-is-not-object-nor-array",
                Data = "abc"
            };

            yield return new JsonInstance
            {
                Name = "root-object-end-of-data",
                Data = @"{""prop"":""abc"""
            };

            yield return new JsonInstance
            {
                Name = "root-object-has-not-closed-markup",
                Data = @"{""prop"":""abc""d"
            };

            yield return new JsonInstance
            {
                Name = "root-object-missing-colon",
                Data = @"{""prop""=""abc""}"
            };

            yield return new JsonInstance
            {
                Name = "root-array-end-of-data",
                Data = @"[""abc"""
            };

            yield return new JsonInstance
            {
                Name = "root-array-has-not-closed-markup",
                Data = @"[""abc""d"
            };

            yield return new JsonInstance
            {
                Name = "value-is-not-recognized",
                Data = @"[abc]"
            };

            yield return new JsonInstance
            {
                Name = "node-text-not-closed-markup",
                Data = @"[""abc]"
            };

            yield return new JsonInstance
            {
                Name = "node-unknown-type-nothing",
                Data = @"[nothing]"
            };

            yield return new JsonInstance
            {
                Name = "node-unknown-type-todo",
                Data = @"[todo]"
            };

            yield return new JsonInstance
            {
                Name = "node-unknown-type-fake",
                Data = @"[fake]"
            };
        }
    }
}
