namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Language;

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
    }
}
