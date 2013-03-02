namespace ClojSharp.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void GetNullIfUndefined()
        {
            Context context = new Context();

            Assert.IsNull(context.GetValue("name"));
        }
    }
}
