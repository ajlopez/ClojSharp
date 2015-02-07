﻿namespace ClojSharp.Core.Tests.Language
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
    public class ListTests
    {
        [TestMethod]
        public void MakeList()
        {
            List list = new List(1, null);

            Assert.AreEqual(1, list.First);
            Assert.IsNull(list.Next);
            Assert.AreSame(EmptyList.Instance, list.Rest);
        }

        [TestMethod]
        public void AddList()
        {
            List list1 = new List(1, new List(2, null));
            List list2 = new List(3, new List(4, null));

            var result = List.AddList(list1, list2);

            Assert.IsNotNull(result);
            Assert.AreEqual("(1 2 3 4)", result.ToString());
        }

        [TestMethod]
        public void EvaluateAddIntegers()
        {
            Context context = new Context();
            context.SetValue("+", new Add());
            List list = new List(new Symbol("+"), new List(1, new List(2, null)));

            Assert.AreEqual(3, list.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateAddSymbols()
        {
            Context context = new Context();
            context.SetValue("+", new Add());
            context.SetValue("one", 1);
            context.SetValue("two", 2);
            List list = new List(new Symbol("+"), new List(new Symbol("one"), new List(new Symbol("two"), null)));

            Assert.AreEqual(3, list.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefIntegerVariable()
        {
            Machine machine = new Machine();
            IContext context = new VarContext(machine);
            context.SetValue("def", new Def());
            List list = new List(new Symbol("def"), new List(new Symbol("one"), new List(1, null)));

            var result = list.Evaluate(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Var));

            var var = (Var)result;

            Assert.AreEqual("one", var.Name);

            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void SimpleListToString()
        {
            List list = new List(1, new List(2, null));

            Assert.AreEqual("(1 2)", list.ToString());
        }

        [TestMethod]
        public void Length()
        {
            List list = new List(1, new List(2, null));

            Assert.AreEqual(2, list.Length);
        }

        [TestMethod]
        public void SimpleListToList()
        {
            List list = new List(1, new List(2, null));
            var result = list.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0]);
            Assert.AreEqual(2, result[1]);
        }

        [TestMethod]
        public void ListWithNilToString()
        {
            List list = new List(null, null);

            Assert.AreEqual("(nil)", list.ToString());
        }

        [TestMethod]
        public void ListWithTwoNilsToString()
        {
            List list = new List(null, new List(null, null));

            Assert.AreEqual("(nil nil)", list.ToString());
        }

        [TestMethod]
        public void EmptyListProperties()
        {
            Assert.IsNull(EmptyList.Instance.First);
            Assert.IsNull(EmptyList.Instance.Next);
            Assert.AreSame(EmptyList.Instance, EmptyList.Instance.Rest);
        }

        [TestMethod]
        public void EmptyListToString()
        {
            Assert.AreEqual("()", EmptyList.Instance.ToString());
        }
    }
}
