﻿namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Language;

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
    }
}