namespace JsonIndex.Samples.HelloWorld
{
    public class HelloWorldSample
    {
        public int Count(string data)
        {
            Index index = Index.Build(data);
            HelloWorldCounter counter = new HelloWorldCounter();

            index.Root.Accept(counter);
            return counter.Count;
        }
    }
}