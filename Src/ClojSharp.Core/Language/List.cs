namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Forms;

    public class List : IEvaluable, ISeq
    {
        private object first;
        private ISeq rest;

        public List(object first, ISeq rest)
        {
            this.first = first;
            this.rest = rest;
        }

        public object First { get { return this.first; } }

        public ISeq Next { get { return this.rest; } }

        public ISeq Rest { get { return this.rest == null ? EmptyList.Instance : this.rest; } }

        public int Length
        {
            get
            {
                if (this.rest == null)
                    return 1;

                return 1 + ((List)this.rest).Length;
            }
        }

        public static List AddList(ISeq list1, List list2)
        {
            if (list1 == null)
                return list2;

            return new List(list1.First, AddList(list1.Next, list2));
        }

        public static List AddItem(ISeq list, object item)
        {
            if (list == null)
                return new List(item, null);

            return new List(list.First, AddItem(list.Next, item));
        }

        public static List FromEnumerable(IEnumerable<object> enumerable)
        {
            if (enumerable == null || enumerable.Count() == 0)
                return null;

            return new List(enumerable.First(), FromEnumerable(enumerable.Skip(1)));
        }

        public object Evaluate(IContext context)
        {
            IForm fn;
            fn = (IForm)((IEvaluable)this.first).Evaluate(context);

            IList<object> arguments = new List<object>();

            for (var args = this.rest; args != null; args = args.Next)
                arguments.Add(args.First);

            return fn.Evaluate(context, arguments);
        }

        public override string ToString()
        {
            return Machine.ToString(this);
        }

        public IList<object> ToList()
        {
            IList<object> result = new List<object>();
            ISeq seq = this;

            while (seq != null) 
            {
                result.Add(seq.First);
                seq = seq.Next;
            }

            return result;
        }
    }
}
