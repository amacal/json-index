using System.Collections;
using System.Collections.Generic;

namespace JsonIndex
{
    public class JsonItemCollection : IEnumerable<JsonItem>
    {
        private readonly Index index;
        private readonly int offset;

        public JsonItemCollection(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public IEnumerator<JsonItem> GetEnumerator()
        {
            int child = this.offset;
            int count = 0;

            while (child > 0)
            {
                yield return new JsonItem(this.index, child, count);
                child = this.index[child].Next;
                count++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
