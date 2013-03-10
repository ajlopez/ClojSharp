namespace ClojSharp.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class RuntimeException : Exception
    {
        public RuntimeException(string message)
            : base(message)
        {
        }
    }
}
