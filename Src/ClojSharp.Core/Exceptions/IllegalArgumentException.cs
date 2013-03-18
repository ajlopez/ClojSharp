namespace ClojSharp.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class IllegalArgumentException : Exception
    {
        public IllegalArgumentException(string message)
            : base(message)
        {
        }
    }
}
