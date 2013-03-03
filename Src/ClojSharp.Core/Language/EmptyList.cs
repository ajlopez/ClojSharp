namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EmptyList : List
    {
        private static EmptyList instance = new EmptyList();

        public static EmptyList Instance { get { return instance; } }

        private EmptyList()
            : base(null, null)
        {
        }

        public override string ToString()
        {
            return "()";
        }
    }
}
