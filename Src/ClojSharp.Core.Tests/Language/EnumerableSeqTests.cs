namespace ClojSharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ClojSharp.Core.Language;

    [TestClass]
    public class EnumerableSeqTests
    {
        [TestMethod]
        public void StringUsingNext()
        {
            var seq = EnumerableSeq.MakeSeq("foo");

            Assert.AreEqual('f', seq.First);
            Assert.AreEqual('o', seq.Next.First);
            Assert.AreEqual('o', seq.Next.Next.First);
            Assert.IsNull(seq.Next.Next.Next);
        }

        [TestMethod]
        public void StringUsingNextInReverse()
        {
            var seq = EnumerableSeq.MakeSeq("foo");

            Assert.AreEqual('o', seq.Next.Next.First);
            Assert.AreEqual('o', seq.Next.First);
            Assert.AreEqual('f', seq.First);
            Assert.IsNull(seq.Next.Next.Next);
        }

        [TestMethod]
        public void StringUsingRest()
        {
            var seq = EnumerableSeq.MakeSeq("foo");

            Assert.AreEqual('f', seq.First);
            Assert.AreEqual('o', seq.Rest.First);
            Assert.AreEqual('o', seq.Rest.Rest.First);
            Assert.IsInstanceOfType(seq.Rest.Rest.Rest, typeof(EmptyList));
        }

        [TestMethod]
        public void StringUsingRestInReverse()
        {
            var seq = EnumerableSeq.MakeSeq("foo");

            Assert.AreEqual('o', seq.Rest.Rest.First);
            Assert.AreEqual('o', seq.Rest.First);
            Assert.AreEqual('f', seq.First);
            Assert.IsInstanceOfType(seq.Rest.Rest.Rest, typeof(EmptyList));
        }
    }
}
