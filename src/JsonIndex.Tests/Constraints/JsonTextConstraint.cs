﻿using System;
using NUnit.Framework;

namespace JsonIndex.Tests.Constraints
{
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

        public void Verify(JsonCollector collector)
        {
            Assert.That(collector.Texts.Items, Is.EquivalentTo(this.texts));
        }
    }
}
