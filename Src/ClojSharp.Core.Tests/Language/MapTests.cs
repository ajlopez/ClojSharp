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
            Map map = new Map(null);
            Map map2 = map.SetValue("name", "foo");

            Assert.AreNotSame(map, map2);

            Assert.AreEqual("foo", map2.GetValue("name"));
            Assert.IsNull(map.GetValue("name"));
        }
    }
}
