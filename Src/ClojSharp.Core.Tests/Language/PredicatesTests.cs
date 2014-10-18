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
        public void IsReal()
        {
            Assert.IsTrue(Predicates.IsReal(1.0));
            Assert.IsTrue(Predicates.IsReal((float)1.0));

            Assert.IsFalse(Predicates.IsReal(1));
            Assert.IsFalse(Predicates.IsReal(null));
            Assert.IsFalse(Predicates.IsReal(string.Empty));
            Assert.IsFalse(Predicates.IsReal("foo"));
        }

        [TestMethod]
        public void IsZero()
        {
            Assert.IsTrue(Predicates.IsZero(0.0));
            Assert.IsTrue(Predicates.IsZero((float)0.0));
            Assert.IsTrue(Predicates.IsZero(0));
            Assert.IsTrue(Predicates.IsZero((short)0));
            Assert.IsTrue(Predicates.IsZero((long)0));
            Assert.IsTrue(Predicates.IsZero((uint)0));
            Assert.IsTrue(Predicates.IsZero((ushort)0));
            Assert.IsTrue(Predicates.IsZero((ulong)0));

            Assert.IsFalse(Predicates.IsZero(1));
            Assert.IsFalse(Predicates.IsZero(null));
            Assert.IsFalse(Predicates.IsZero(string.Empty));
            Assert.IsFalse(Predicates.IsZero("foo"));
            Assert.IsFalse(Predicates.IsZero(1));
            Assert.IsFalse(Predicates.IsZero(1.2));
        }

        [TestMethod]
        public void IsInteger()
        {
            Assert.IsTrue(Predicates.IsInteger(1));
            Assert.IsTrue(Predicates.IsInteger((uint)1));
            Assert.IsTrue(Predicates.IsInteger((short)1));
            Assert.IsTrue(Predicates.IsInteger((ushort)1));
            Assert.IsTrue(Predicates.IsInteger((long)1));
            Assert.IsTrue(Predicates.IsInteger((ulong)1));

            Assert.IsFalse(Predicates.IsInteger(1.0));
            Assert.IsFalse(Predicates.IsInteger(null));
            Assert.IsFalse(Predicates.IsInteger(string.Empty));
            Assert.IsFalse(Predicates.IsInteger("foo"));
        }

        [TestMethod]
        public void IsNumeric()
        {
            Assert.IsTrue(Predicates.IsNumeric(1));
            Assert.IsTrue(Predicates.IsNumeric(1.0));
            Assert.IsTrue(Predicates.IsNumeric((uint)1));
            Assert.IsTrue(Predicates.IsNumeric((short)1));
            Assert.IsTrue(Predicates.IsNumeric((ushort)1));
            Assert.IsTrue(Predicates.IsNumeric((long)1));
            Assert.IsTrue(Predicates.IsNumeric((ulong)1));

            Assert.IsFalse(Predicates.IsNumeric(null));
            Assert.IsFalse(Predicates.IsNumeric(string.Empty));
            Assert.IsFalse(Predicates.IsNumeric("foo"));
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
