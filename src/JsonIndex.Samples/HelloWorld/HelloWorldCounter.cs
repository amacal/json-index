namespace JsonIndex.Samples.HelloWorld
{
    public class HelloWorldCounter : JsonVisitorBase
    {
        private int count;

        public int Count
        {
            get { return this.count; }
        }

        public override void Visit(JsonText text)
        {
            if (text.GetValue().Contains("hello-world") == true)
            {
                this.count++;
            }
        }
    }
}