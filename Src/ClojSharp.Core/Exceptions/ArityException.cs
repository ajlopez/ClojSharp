namespace ClojSharp.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ArityException : Exception
    {
        public ArityException(Type type, int arity)
            : base(string.Format("Wrong number of args ({0}) passed to {1}", arity, type.FullName))
        {
        }
    }
}
