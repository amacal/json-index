using System.Collections.Generic;

namespace JsonIndex.Tests.Scenarios
{
    public static class ViolationScenario
    {
        public static IEnumerable<JsonInstance> All()
        {
            yield return new JsonInlineSource
            {
                Name = "root-is-not-object-nor-array",
                Data = "abc"
            };

            yield return new JsonInlineSource
            {
                Name = "root-object-end-of-data",
                Data = @"{""prop"":""abc"""
            };

            yield return new JsonInlineSource
            {
                Name = "root-object-has-not-closed-markup",
                Data = @"{""prop"":""abc""d"
            };

            yield return new JsonInlineSource
            {
                Name = "root-object-missing-colon",
                Data = @"{""prop""=""abc""}"
            };

            yield return new JsonInlineSource
            {
                Name = "root-array-end-of-data",
                Data = @"[""abc"""
            };

            yield return new JsonInlineSource
            {
                Name = "root-array-has-not-closed-markup",
                Data = @"[""abc""d"
            };

            yield return new JsonInlineSource
            {
                Name = "value-is-not-recognized",
                Data = @"[abc]"
            };

            yield return new JsonInlineSource
            {
                Name = "node-text-not-closed-markup",
                Data = @"[""abc]"
            };

            yield return new JsonInlineSource
            {
                Name = "node-unknown-type-nothing",
                Data = @"[nothing]"
            };

            yield return new JsonInlineSource
            {
                Name = "node-unknown-type-todo",
                Data = @"[todo]"
            };

            yield return new JsonInlineSource
            {
                Name = "node-unknown-type-fake",
                Data = @"[fake]"
            };
        }
    }
}
