using System.Collections.Generic;
using System.Linq;

namespace JsonIndex
{
    public class JObject
    {
        private readonly Index index;
        private readonly int offset;

        public JObject(Index index, int offset)
        {
            this.index = index;
            this.offset = offset;
        }

        public IEnumerable<JProperty> Properties()
        {
            IndexEntry entry = this.index[this.offset];

            int child = entry.First;
            int count = 0;

            while (child > 0)
            {
                if (count % 2 == 0)
                {
                    yield return new JProperty(this.index, child);
                }

                child = this.index[child].Next;
                count++;
            }
        }
    }
}
