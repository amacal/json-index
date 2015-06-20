using System;

namespace JsonIndex
{
    internal static class JsonContainer
    {
        public static JsonNode GetValue(Index index, int offset)
        {
            if (offset == 0)
            {
                return new JsonTerminator();
            }

            switch (index[offset].Type)
            {
                case IndexType.Object:
                    return new JsonObject(index, offset);

                case IndexType.Property:
                    return new JsonTerminator();

                case IndexType.Array:
                    return new JsonArray(index, offset);

                case IndexType.Text:
                    return new JsonText(index, offset);

                case IndexType.Number:
                    return new JsonNumber(index, offset);

                case IndexType.True:
                    return new JsonTrue();

                case IndexType.False:
                    return new JsonFalse();

                case IndexType.Null:
                    return new JsonNull();

                default:
                    throw new NotSupportedException();
            }
        }
    }
}