namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Namespace
    {
        private string name;

        public Namespace(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
