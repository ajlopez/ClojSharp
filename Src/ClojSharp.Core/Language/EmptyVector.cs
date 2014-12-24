namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EmptyVector : Vector
    {
        private static EmptyVector instance = new EmptyVector();

        private EmptyVector()
            : base(null, null)
        {
        }

        public static EmptyVector Instance { get { return instance; } }

        public override string ToString()
        {
            return "[]";
        }
    }
}
