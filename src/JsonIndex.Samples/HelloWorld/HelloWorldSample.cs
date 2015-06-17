namespace JsonIndex.Samples.HelloWorld
{
    public class HelloWorldSample
    {
        public int Count(string data)
        {
            Index index = IndexFactory.Build(data);
            HelloWorldCounter counter = new HelloWorldCounter();

            index.Root.Accept(counter);
            return counter.Count;
        }
    }
}