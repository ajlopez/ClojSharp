namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnumerableSeq : ISeq
    {
        private IEnumerator enumerator;
        private bool expanded;
        private object first;
        private ISeq rest;

        public static ISeq MakeSeq(IEnumerable value)
        {
            return MakeSeq(value.GetEnumerator());
        }

        private static ISeq MakeSeq(IEnumerator enumerator)
        {
            if (enumerator.MoveNext())
                return new EnumerableSeq(enumerator);

            return EmptyList.Instance;
        }

        private EnumerableSeq(IEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        private void Expand()
        {
            this.first = this.enumerator.Current;
            this.rest = MakeSeq(this.enumerator);

            this.expanded = true;
        }

        public object First
        {
            get 
            {
                if (!this.expanded)
                    this.Expand();

                return this.first;
            }
        }

        public ISeq Next
        {
            get
            {
                if (!this.expanded)
                    this.Expand();

                if (this.rest is EmptyList)
                    return null;

                return this.rest;
            }
        }

        public ISeq Rest
        {
            get
            {
                if (!this.expanded)
                    this.Expand();

                return this.rest;
            }
        }
    }
}
