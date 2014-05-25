namespace ClojSharp.Core.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;
    using ClojSharp.Core.Language;

    public abstract class BaseUnaryForm : BaseForm
    {
        public override int RequiredArity { get { return 1; } }

        public override bool VariableArity { get { return false; } }
    }
}
