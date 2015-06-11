# json-index
JSON indexing library.

# Goal
The library aims not to be used as a replacement for any deserializer. It just offers another faster low level way of reading the JSON.

# Usage
The following code is responsible to counting all occurrences of "hello-world" in all text nodes:
```csahrp
using JsonIndex;

public class HelloWorld
{
  public int Count(string json)
  {
    Index index = Index.Build(json);
    JsonVisitor visitor = new HelloWorldCounter();

    index.Visit(visitor);
    return visitor.Count;
  }
}

public class HelloWorldCounter : JsonVisitor
{
  private int count;

  public void Visit(JsonObject instance)
  {
    foreach (JsonProperty property in instance.Properties)
    {
      property.Accept(this);
    }
  }
  
  public int Count
  {
    get { return this.count; }
  }

  public void Visit(JsonProperty property)
  {
    property.GetValue().Accept(this);
  }

  public void Visit(JsonArray array)
  {
    foreach (JsonNode node in array.GetItems())
    {
        node.Accept(this);
    }
  }

  public void Visit(JsonItem item)
  {
    item.GetValue().Accept(this);
  }

  public void Visit(JsonText text)
  {
    if (text.GetValue().Contains("hello-world") == true)
    {
      this.count++;
    }
  }

  public void Visit(JsonNumber number)
  {
  }

  public void Visit(JsonTrue value)
  {
  }

  public void Visit(JsonFalse value)
  {
  }

  public void Visit(JsonNull value)
  {
  }
}
```
