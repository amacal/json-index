# json-index
JSON indexing library.

# Goal
The library aims not to be used as a replacement for any deserializer. It just offers another faster low level way of reading the JSON.

# Usage
The following code is responsible for counting all occurrences of "hello-world" in all text nodes:
```csharp
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
```
