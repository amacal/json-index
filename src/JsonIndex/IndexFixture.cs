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
        [TestCaseSource(typeof(JsonScenarioFactory), "Empty")]
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
            private readonly List<string> propertyNames;
            private readonly List<string> propertyData;

            public Collector()
            {
                this.propertyNames = new List<string>();
                this.propertyData = new List<string>();
            }

            public IEnumerable<string> PropertyNames
            {
                get { return this.propertyNames; }
            }

            public IEnumerable<string> PropertyData
            {
                get { return this.propertyData; }
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
                this.propertyNames.Add(property.GetName());
                this.propertyData.Add(property.GetText());

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
        
        public interface JsonConstraint
        {
            string Describe();

            void Verify(Collector collector);
        }

        public class JsonPropertyNameConstraint : JsonConstraint
        {
            private readonly string[] names;

            public JsonPropertyNameConstraint(params string[] names)
            {
                this.names = names;
            }

            public string Describe()
            {
                return "property-names: " + String.Join(", ", this.names);
            }

            public void Verify(Collector collector)
            {
                Assert.That(collector.PropertyNames, Is.EquivalentTo(this.names));
            }
        }

        public class JsonPropertyDataConstraint : JsonConstraint
        {
            private readonly string[] data;

            public JsonPropertyDataConstraint(params string[] data)
            {
                this.data = data;
            }

            public string Describe()
            {
                return "property-data: " + String.Join(", ", this.data);
            }

            public void Verify(Collector collector)
            {
                Assert.That(collector.PropertyData, Is.EquivalentTo(this.data));
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
            public static IEnumerable<JsonScenario> Empty()
            {
                JsonInstance instance = new JsonInstance
                {
                    Name = "empty",
                    Data = "{}"
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonPropertyNameConstraint()
                };

                yield return new JsonScenario
                {
                    Instance = instance,
                    Constraint = new JsonPropertyDataConstraint()
                };
            }
        }
    }
}
