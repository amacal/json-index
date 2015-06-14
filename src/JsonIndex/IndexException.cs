using System;

namespace JsonIndex
{
    public class IndexException : Exception
    {
        public IndexException(string message)
            : base(message)
        {
        }
    }
}