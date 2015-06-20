using System;
using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonTerminator : JsonNode
    {
        public static readonly JsonTerminator Instance;

        static JsonTerminator()
        {
            Instance = new JsonTerminator();
        }

        private JsonTerminator()
        {
        }

        public IEnumerable<JsonNode> GetChildren()
        {
            yield break;
        }

        public void Accept(JsonVisitor visitor)
        {
        }

        public override string ToString()
        {
            return String.Empty;
        }
    }
}
