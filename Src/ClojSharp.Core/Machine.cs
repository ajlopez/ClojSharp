﻿namespace ClojSharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;
    using ClojSharp.Core.SpecialForms;
    using ClojSharp.Core.Language;

    public class Machine
    {
        private Context root;

        public Machine()
        {
            this.root = new Context();
            this.root.SetValue("def", new Def());
            this.root.SetValue("+", new Add());
        }

        public Context RootContext { get { return this.root; } }

        public object Evaluate(object obj, Context context)
        {
            if (obj is IEvaluable)
                return ((IEvaluable)obj).Evaluate(context);

            return obj;
        }
    }
}
