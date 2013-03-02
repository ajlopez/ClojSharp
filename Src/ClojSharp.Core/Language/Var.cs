namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Var
    {
        private string name;

        public Var(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
