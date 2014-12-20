namespace ClojSharp.Core.Tests.SpecialForms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.Language;
    using ClojSharp.Core.SpecialForms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DefTests
    {
        [TestMethod]
        public void DefIntegerVariable()
        {
            Machine machine = new Machine();
            IContext context = new VarContext(machine);
            Symbol symbol = new Symbol("one");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol, 1 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void DefVariableWithoutInitialValue()
        {
            Machine machine = new Machine();
            IContext context = new VarContext(machine);
            Symbol symbol = new Symbol("one");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void DefVariableFromASymbol()
        {
            Machine machine = new Machine();
            IContext context = new VarContext(machine);
            context.SetValue("uno", 1);
            Symbol symbol = new Symbol("one");
            Symbol uno = new Symbol("uno");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol, uno });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void DefVariableFromAListExpression()
        {
            Machine machine = new Machine();
            IContext context = new VarContext(machine);
            context.SetValue("add", new Add());
            Symbol symbol = new Symbol("three");
            List list = new List(new Symbol("add"), new List(1, new List(2, null)));
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol, list });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("three", var.Name);

            Assert.AreEqual(3, context.GetValue("three"));
        }

        [TestMethod]
        public void DefIntegerVariableAtRootContext()
        {
            Machine machine = new Machine();
            IContext root = new VarContext(machine);
            IContext context = new Context(root);
            Symbol symbol = new Symbol("one");
            Def def = new Def();

            var result = def.Evaluate(context, new object[] { symbol, 1 });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(1, root.GetValue("one"));
        }
    }
}
