namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Keyword
    {
        private string name;

        public Keyword(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public override string ToString()
        {
            return ":" + this.name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Keyword)
            {
                var keyword = (Keyword)obj;

                return this.Name.Equals(keyword.Name);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + 17;
        }
    }
}
