using System;

namespace JsonIndex
{
    internal static class JsonContainer
    {
        public static JsonNode GetValue(Index index, int offset)
        {
            if (offset == 0)
            {
                return JsonTerminator.Instance;
            }

            switch (index[offset].Type)
            {
                case IndexType.Object:
                    return new JsonObject(index, offset);

                case IndexType.Property:
                    return JsonTerminator.Instance;

                case IndexType.Array:
                    return new JsonArray(index, offset);

                case IndexType.Text:
                    return new JsonText(index, offset);

                case IndexType.Number:
                    return new JsonNumber(index, offset);

                case IndexType.True:
                    return JsonTrue.Instance;

                case IndexType.False:
                    return JsonFalse.Instance;

                case IndexType.Null:
                    return JsonNull.Instance;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}