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
    public class SetTests
    {
        [TestMethod]
        public void CreateSet()
        {
            Set set = new Set(new object[] { new Keyword("one"), new Keyword("two") });

            Assert.IsTrue(set.HasKey(new Keyword("one")));
            Assert.IsTrue(set.HasKey(new Keyword("two")));
            Assert.IsFalse(set.HasKey(new Keyword("three")));
        }

        [TestMethod]
        public void CreateSetUsingCreate()
        {
            Set set = Set.Create(new object[] { new Keyword("one"), new Keyword("two") });

            Assert.IsTrue(set.HasKey(new Keyword("one")));
            Assert.IsTrue(set.HasKey(new Keyword("two")));
            Assert.IsFalse(set.HasKey(new Keyword("three")));
        }

        [TestMethod]
        public void SetToString()
        {
            Set set = Set.Create(new object[] { new Keyword("one"), new Keyword("two") });

            Assert.AreEqual("#{:one :two}", set.ToString());
        }

        [TestMethod]
        public void CreateEmptySet()
        {
            Set set = new Set(null);

            Assert.IsFalse(set.HasKey("a"));
        }

        [TestMethod]
        public void AddValue()
        {
            Set set = new Set(new object[] { "bar" });
            Set set2 = set.Add("foo");

            Assert.AreNotSame(set, set2);

            Assert.IsTrue(set2.HasKey("foo"));
            Assert.IsTrue(set2.HasKey("bar"));
            Assert.IsFalse(set.HasKey("foo"));
            Assert.IsTrue(set.HasKey("bar"));
        }

        [TestMethod]
        public void AddExistingValue()
        {
            Set set = new Set(new object[] { "bar", "foo" });
            Set set2 = set.Add("foo");

            Assert.AreSame(set, set2);

            Assert.IsTrue(set2.HasKey("foo"));
            Assert.IsTrue(set2.HasKey("bar"));
            Assert.IsTrue(set.HasKey("foo"));
            Assert.IsTrue(set.HasKey("bar"));
        }

        [TestMethod]
        public void RemoveValue()
        {
            Set set = new Set(new object[] { "foo", "bar" });
            Set set2 = set.Remove("foo");

            Assert.AreNotSame(set, set2);

            Assert.IsFalse(set2.HasKey("foo"));
            Assert.IsTrue(set2.HasKey("bar"));
            Assert.IsTrue(set.HasKey("foo"));
            Assert.IsTrue(set.HasKey("bar"));
        }

        [TestMethod]
        public void RemoveNonExistingValue()
        {
            Set set = new Set(new object[] { "bar", "foo" });
            Set set2 = set.Remove("foobar");

            Assert.AreSame(set, set2);

            Assert.IsTrue(set2.HasKey("foo"));
            Assert.IsTrue(set2.HasKey("bar"));
            Assert.IsTrue(set.HasKey("foo"));
            Assert.IsTrue(set.HasKey("bar"));
        }
    }
}
