namespace ClojSharp.Core.Tests.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConsTests
    {
        [TestMethod]
        public void ConsInteger()
        {
            Cons cons = new Cons();
            var result= cons.Evaluate(null, new object[] { 1, null });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ISeq));
            
            var seq = (ISeq)result;
            Assert.AreEqual(1, seq.First);
            Assert.IsNull(seq.Next);
        }
    }
}
