namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PredicatesTests
    {
        [TestMethod]
        public void Equals()
        {
            Assert.IsTrue(Predicates.Equals(null, null));
            Assert.IsTrue(Predicates.Equals(1, 1));
            Assert.IsTrue(Predicates.Equals(1, 1.0));
            Assert.IsTrue(Predicates.Equals(1.0, 1.0));
            Assert.IsTrue(Predicates.Equals(1.0, 1));
            Assert.IsTrue(Predicates.Equals("foo", "FOO".ToLower()));

            Assert.IsFalse(Predicates.Equals(1, null));
            Assert.IsFalse(Predicates.Equals("foo", null));
            Assert.IsFalse(Predicates.Equals(null, 1));
            Assert.IsFalse(Predicates.Equals(null, "foo"));
            Assert.IsFalse(Predicates.Equals(1, 2));
            Assert.IsFalse(Predicates.Equals(1.0, 2));
            Assert.IsFalse(Predicates.Equals(1, 2.0));
            Assert.IsFalse(Predicates.Equals("foo", "FOO"));
        }

        [TestMethod]
        public void IsFalse()
        {
            Assert.IsTrue(Predicates.IsFalse(null));
            Assert.IsTrue(Predicates.IsFalse(false));

            Assert.IsFalse(Predicates.IsFalse(1));
            Assert.IsFalse(Predicates.IsFalse(0));
            Assert.IsFalse(Predicates.IsFalse(0.0));
            Assert.IsFalse(Predicates.IsFalse(1.0));
            Assert.IsFalse(Predicates.IsFalse(string.Empty));
            Assert.IsFalse(Predicates.IsFalse(" "));
            Assert.IsFalse(Predicates.IsFalse("foo"));
        }

        [TestMethod]
        public void IsTrue()
        {
            Assert.IsFalse(Predicates.IsTrue(null));
            Assert.IsFalse(Predicates.IsTrue(false));

            Assert.IsTrue(Predicates.IsTrue(1));
            Assert.IsTrue(Predicates.IsTrue(0));
            Assert.IsTrue(Predicates.IsTrue(0.0));
            Assert.IsTrue(Predicates.IsTrue(1.0));
            Assert.IsTrue(Predicates.IsTrue(string.Empty));
            Assert.IsTrue(Predicates.IsTrue(" "));
            Assert.IsTrue(Predicates.IsTrue("foo"));
        }
    }
}
