﻿namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Vector : IEvaluable, IObject, ISeq
    {
        private object[] elements;
        private Map metadata;

        public Vector(IList<object> elements)
            : this(elements, null)
        {
        }

        internal Vector(IList<object> elements, Map metadata)
        {
            if (elements != null)
                this.elements = elements.ToArray();

            this.metadata = metadata;
        }

        internal Vector(object[] elements, Map metadata)
        {
            this.elements = elements;
            this.metadata = metadata;
        }

        public IList<object> Elements { get { return this.elements; } }

        public int Length { get { return this.elements.Length; } }

        public object First
        {
            get 
            { 
                if (this.elements == null || this.elements.Length == 0) 
                    return null; 
                
                return this.elements[0]; 
            }
        }

        public ISeq Next
        {
            get 
            { 
                if (this.elements == null || this.elements.Length <= 1)
                    return null; 
                
                return EnumerableSeq.MakeSeq(this.elements.Skip(1)); 
            }
        }

        public ISeq Rest
        {
            get 
            { 
                var next = this.Next; 
                
                if (next == null) 
                    return EmptyList.Instance; 
                
                return next; 
            }
        }

        public Map Metadata
        {
            get { return this.metadata; }
        }

        public static Vector AddItem(Vector vector, object item)
        {
            if (vector == null || vector.elements == null || vector.elements.Length == 0)
                return new Vector(new object[] { item });

            var newlist = new List<object>(vector.elements);
            newlist.Add(item);

            return new Vector(newlist);
        }

        public static Vector Create(ISeq seq)
        {
            var list = new List<object>();

            while (seq != null)
            {
                list.Add(seq.First);
                seq = seq.Next;
            }

            return new Vector(list);
        }

        public Vector Associate(IList<object> keyvalues)
        {
            IList<object> newelements = new List<object>(this.elements);

            for (int k = 0; k < keyvalues.Count; k += 2)
            {
                int index = (int)keyvalues[k];
                object value = keyvalues[k + 1];

                if (newelements.Count > index)
                    newelements[index] = value;
                else
                    newelements.Add(value);
            }

            return new Vector(newelements, this.metadata);
        }

        public object Evaluate(IContext context)
        {
            if (this.elements == null || this.elements.Length == 0)
                return this;

            object[] values = new object[this.elements.Length];

            for (var k = 0; k < values.Length; k++)
                values[k] = Machine.Evaluate(this.elements[k], context);

            return new Vector(values, this.metadata);
        }

        public override string ToString()
        {
            string result = "[";

            if (this.elements != null)
                for (int k = 0; k < this.elements.Length; k++)
                {
                    var expr = this.elements[k];

                    if (k > 0)
                        result += " ";

                    result += expr.ToString();
                }

            return result + "]";
        }

        public IObject WithMetadata(Map map)
        {
            Map newmap = map;

            if (this.metadata != null)
                newmap = this.metadata.Merge(map);

            return new Vector(this.elements, newmap);
        }
    }
}
