namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void MakeVector()
        {
            Vector vector = new Vector(new object[] { 1, 2, });

            Assert.IsNotNull(vector.Elements);
            Assert.AreEqual(2, vector.Elements.Count);
            Assert.AreEqual(1, vector.Elements[0]);
            Assert.AreEqual(2, vector.Elements[1]);
        }

        [TestMethod]
        public void First()
        {
            Vector vector = new Vector(new object[] { 1, 2 });

            Assert.AreEqual(1, vector.First);
        }

        [TestMethod]
        public void Next()
        {
            Vector vector = new Vector(new object[] { 1, 2 });

            var result = vector.Next;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));
            Assert.AreEqual("(2)", result.ToString());

            Assert.AreEqual(1, vector.First);
        }

        [TestMethod]
        public void NextNull()
        {
            Vector vector = new Vector(new object[] { 2 });

            Assert.IsNull(vector.Next);
        }

        [TestMethod]
        public void Rest()
        {
            Vector vector = new Vector(new object[] { 1, 2 });

            var result = vector.Rest;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));
            Assert.AreEqual("(2)", result.ToString());

            Assert.AreEqual(1, vector.First);
        }

        [TestMethod]
        public void RestEmpty()
        {
            Vector vector = new Vector(new object[] { 2 });

            var result = vector.Rest;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(EmptyList));
            Assert.AreEqual("()", result.ToString());
            Assert.IsNull(result.First);
            Assert.AreSame(result, result.Rest);
            Assert.IsNull(result.Next);
        }
    }
}
