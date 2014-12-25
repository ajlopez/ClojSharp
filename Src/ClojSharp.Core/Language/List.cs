namespace ClojSharp.Core.Language
{
    using System;
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

        public static List FromSequence(ISeq seq)
        {
            if (seq == null)
                return null;

            return new List(seq.First, FromSequence(seq.Next));
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
            StringBuilder sb = new StringBuilder();

            sb.Append("(");
            sb.Append(Machine.ToString(this.first));

            for (var rest = this.rest; rest != null; rest = rest.Next)
            {
                sb.Append(" ");
                sb.Append(Machine.ToString(rest.First));
            }
            
            sb.Append(")");

            return sb.ToString();
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
