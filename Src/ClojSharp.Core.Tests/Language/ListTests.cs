namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ListTests
    {
        [TestMethod]
        public void MakeList()
        {
            List list = new List(1, 2);

            Assert.AreEqual(1, list.First);
            Assert.AreEqual(2, list.Rest);
        }
    }
}
