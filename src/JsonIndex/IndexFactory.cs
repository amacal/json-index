using System.Collections.Generic;

namespace JsonIndex
{
    public static class IndexFactory
    {
        public static Index Build(string data)
        {
            IndexBuilder builder = new IndexBuilder(data, new IndexSettings());
            IndexResult result = builder.Build();

            if (result.IsSuccessful() == false)
            {
                throw new IndexException("The index could not be built.", result.Violations);
            }

            return result.Index;
        }

        public static Index Build(string data, IndexSettings settings)
        {
            IndexBuilder builder = new IndexBuilder(data, settings);
            IndexResult result = builder.Build();

            if (result.IsSuccessful() == false)
            {
                throw new IndexException("The index could not be built.", result.Violations);
            }

            return result.Index;
        }

        public static IEnumerable<Index> Scan(string data)
        {
            IndexBuilder builder;
            IndexResult result;

            for (int i = 0; i < data.Length; i++)
            {
                switch (data[i])
                {
                    case '{':
                    case '[':

                        builder = new IndexBuilder(data, new IndexSettings());
                        result = builder.Build(i);

                        if (result.IsSuccessful() == true)
                        {
                            i = result.Index[0].End;
                            yield return result.Index;
                        }

                        break;
                }
            }
        }
    }
}
