namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void CreateMap()
        {
            Map map = new Map(new object[] { new Keyword("one"), 1, new Keyword("two"), 2 });

            Assert.AreEqual(1, map.GetValue(new Keyword("one")));
            Assert.AreEqual(2, map.GetValue(new Keyword("two")));
        }

        [TestMethod]
        public void CreateMapUsingCreate()
        {
            Map map = Map.Create(new object[] { new Keyword("one"), 1, new Keyword("two"), 2 });

            Assert.AreEqual(1, map.GetValue(new Keyword("one")));
            Assert.AreEqual(2, map.GetValue(new Keyword("two")));
        }

        [TestMethod]
        public void MapToString()
        {
            Map map = Map.Create(new object[] { new Keyword("one"), 1, new Keyword("two"), 2 });

            Assert.AreEqual("{:one 1 :two 2}", map.ToString());
        }

        [TestMethod]
        public void CreateEmptyMap()
        {
            Map map = new Map(null);

            Assert.IsNull(map.GetValue("a"));
        }

        [TestMethod]
        [ExpectedException(typeof(RuntimeException))]
        public void RaiseIfCreateMapWithOddKeyValues()
        {
            new Map(new object[] { new Keyword("a"), 1, new Keyword("b") });
        }

        [TestMethod]
        public void SetValue()
        {
            Map map = new Map(new object[] { "age", 800 });
            Map map2 = map.SetValue("name", "Adam");

            Assert.AreNotSame(map, map2);

            Assert.AreEqual("Adam", map2.GetValue("name"));
            Assert.AreEqual(800, map2.GetValue("age"));
            Assert.IsNull(map.GetValue("name"));
        }

        [TestMethod]
        public void RemoveValue()
        {
            Map map = Map.Create(new object[] { new Keyword("one"), 1, new Keyword("two"), 2, new Keyword("three"), 3 });
            Map map2 = map.RemoveValue(new Keyword("two"));

            Assert.IsNotNull(map2);

            Assert.IsTrue(map.HasValue(new Keyword("one")));
            Assert.IsTrue(map.HasValue(new Keyword("two")));
            Assert.IsTrue(map.HasValue(new Keyword("three")));

            Assert.IsTrue(map2.HasValue(new Keyword("one")));
            Assert.IsFalse(map2.HasValue(new Keyword("two")));
            Assert.IsTrue(map2.HasValue(new Keyword("three")));

            Assert.AreEqual(1, map2.GetValue(new Keyword("one")));
            Assert.AreEqual(3, map2.GetValue(new Keyword("three")));
        }

        [TestMethod]
        public void RemoveNonExistingValue()
        {
            Map map = Map.Create(new object[] { new Keyword("one"), 1, new Keyword("two"), 2, new Keyword("three"), 3 });
            Map map2 = map.RemoveValue(new Keyword("ten"));

            Assert.IsNotNull(map2);
            Assert.AreSame(map, map2);
        }
    }
}
