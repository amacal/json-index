using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace JsonIndex
{
    [TestFixture]
    public class IndexFixture
    {
        [Test]
        [TestCaseSource(typeof(JsonScenarioFactory), "EmptyObject")]
        [TestCaseSource(typeof(JsonScenarioFactory), "EmptyArray")]
        [TestCaseSource(typeof(JsonScenarioFactory), "TextProperties")]
        [TestCaseSource(typeof(JsonScenarioFactory), "NumberProperties")]
        [TestCaseSource(typeof(JsonScenarioFactory), "MixedArray")]
        public void IndexingAndVisitingShouldExtractTheStructure(JsonScenario scenario)
        {
            // arrange
            Collector collector = new Collector();
            Index index = Index.Build(scenario.Instance.Data);

            // act
            index.Root.Accept(collector);

            // assert
            scenario.Constraint.Verify(collector);
        }

        public class JsonInstance
        {
            public string Name { get; set; }

            public string Data { get; set; }
        }

        public class Collector : JsonVisitor
        {
            private readonly List<string> data;
            private readonly List<string> names;
            private readonly List<string> texts;
            private readonly List<string> numbers;

            public Collector()
            {
                this.data = new List<string>();
                this.names = new List<string>();
                this.texts = new List<string>();
                this.numbers = new List<string>();
            }

            public IEnumerable<string> Data
            {
                get { return this.data; }
            }

            public IEnumerable<string> Names
            {
                get { return this.names; }
            }

            public IEnumerable<string> Texts
            {
                get { return this.texts; }
            }

            public IEnumerable<string> Numbers
            {
                get { return this.numbers; }
            }

            public void Visit(JsonObject instance)
            {
                foreach (JsonProperty property in instance.GetProperties())
                {
                    property.Accept(this);
                }
            }

            public void Visit(JsonProperty property)
            {
                this.names.Add(property.GetName());
                this.data.Add(property.GetText());

                property.GetValue().Accept(this);
            }

            public void Visit(JsonArray array)
            {
                foreach (JsonNode node in array.GetItems())
                {
                    node.Accept(this);
                }
            }

            public void Visit(JsonText text)
            {
                this.texts.Add(text.GetValue());
            }

            public void Visit(JsonNumber number)
            {
                this.numbers.Add(number.GetValue());
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

        public interface JsonConstraint
        {
            string Describe();

            void Verify(Collector collector);
        }

        public class JsonNameConstraint : JsonConstraint
        {
            private readonly string[] names;

            public JsonNameConstraint(params string[] names)
            {
                this.names = names;
            }

            public string Describe()
            {
                return "names: " + String.Join(", ", this.names);
            }

            public void Verify(Collector collector)
            {
                Assert.That(collector.Names, Is.EquivalentTo(this.names));
            }
        }

        public class JsonDataConstraint : JsonConstraint
        {
            private readonly string[] data;

            public JsonDataConstraint(params string[] data)
            {
                this.data = data;
            }

            public string Describe()
            {
                return "data: " + String.Join(", ", this.data);
            }

            public void Verify(Collector collector)
            {
                Assert.That(collector.Data, Is.EquivalentTo(this.data));
            }
        }

        public class JsonTextConstraint : JsonConstraint
        {
            private readonly string[] texts;

            public JsonTextConstraint(params string[] texts)
            {
                this.texts = texts;
            }

            public string Describe()
            {
                return "texts: " + String.Join(", ", this.texts);
            }

            public void Verify(Collector collector)
            {
                Assert.That(collector.Texts, Is.EquivalentTo(this.texts));
            }
        }

        public class JsonNumberConstraint : JsonConstraint
        {
            private readonly string[] numbers;

            public JsonNumberConstraint(params string[] numbers)
            {
                this.numbers = numbers;
            }

            public string Describe()
            {
                return "numbers: " + String.Join(", ", this.numbers);
            }

            public void Verify(Collector collector)
            {
                Assert.That(collector.Numbers, Is.EquivalentTo(this.numbers));
            }
        }

        public class JsonScenario
        {
            public JsonInstance Instance { get; set; }

            public JsonConstraint Constraint { get; set; }

            public override string ToString()
            {
                return String.Join(" | ", this.Instance.Name, this.Constraint.Describe());
            }
        }

        public static class JsonScenarioFactory
        {
            public static IEnumerable<JsonScenario> EmptyObject()
            {
                JsonInstance instance = new JsonInstance
                {
                    Name = "empty-object",
                    Data = "{}"
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonNameConstraint()
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonDataConstraint()
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonTextConstraint()
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonNumberConstraint()
                };
            }

            public static IEnumerable<JsonScenario> EmptyArray()
            {
                JsonInstance instance = new JsonInstance
                {
                    Name = "empty-array",
                    Data = @"{""property"":[]}"
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonDataConstraint("[]")
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonTextConstraint()
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonNumberConstraint()
                };
            }

            public static IEnumerable<JsonScenario> TextProperties()
            {
                JsonInstance instance = new JsonInstance
                {
                    Name = "text-properties",
                    Data = @"{""prop-a"":""value-a"",""prop-b"":""value-b""}"
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonNameConstraint("prop-a", "prop-b")
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonDataConstraint("value-a", "value-b")
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonTextConstraint("value-a", "value-b")
                };
            }

            public static IEnumerable<JsonScenario> NumberProperties()
            {
                JsonInstance instance = new JsonInstance
                {
                    Name = "text-properties",
                    Data = @"{""prop-a"":123,""prop-b"":123.456,""prop-c"":-123,""prop-d"":12.13e-7}"
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonNameConstraint("prop-a", "prop-b", "prop-c", "prop-d")
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonDataConstraint("123", "123.456", "-123", "12.13e-7")
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonNumberConstraint("123", "123.456", "-123", "12.13e-7")
                };
            }

            public static IEnumerable<JsonScenario> MixedArray()
            {
                JsonInstance instance = new JsonInstance
                {
                    Name = "mixed-array",
                    Data = @"{""property"":[""abc"",123,true,false,null,{},[]]}"
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonTextConstraint("abc")
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonNumberConstraint("123")
                };
            }
        }
    }
}
