namespace JsonIndex.Tests
{
    public interface JsonConstraint
    {
        string Describe();

        void Verify(JsonCollector collector);
    }
}
