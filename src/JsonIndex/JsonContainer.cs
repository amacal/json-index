using System;

namespace JsonIndex
{
    internal static class JsonContainer
    {
        public static JsonNode GetValue(Index index, int offset)
        {
            switch (index[offset + 1].Type)
            {
                case IndexType.Object:
                    return new JsonObject(index, offset + 1);

                case IndexType.Array:
                    return new JsonArray(index, offset + 1);

                case IndexType.Text:
                    return new JsonText(index, offset + 1);

                case IndexType.Number:
                    return new JsonNumber(index, offset + 1);

                case IndexType.True:
                    return new JsonTrue();

                case IndexType.False:
                    return new JsonTrue();

                case IndexType.Null:
                    return new JsonTrue();

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
