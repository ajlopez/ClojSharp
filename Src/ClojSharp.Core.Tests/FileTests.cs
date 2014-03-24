namespace ClojSharp.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [DeploymentItem("Files", "Files")]
    public class FileTests
    {
        [TestMethod]
        public void EvaluateEmpty()
        {
            Machine machine = new Machine();
            Assert.IsNull(machine.EvaluateFile("Files\\empty.clj"));
        }

        [TestMethod]
        public void EvaluateOne()
        {
            Machine machine = new Machine();
            Assert.AreEqual(1, machine.EvaluateFile("Files\\one.clj"));
        }

        [TestMethod]
        public void EvaluateTrue()
        {
            Machine machine = new Machine();
            Assert.AreEqual(true, machine.EvaluateFile("Files\\true.clj"));
        }
    }
}
