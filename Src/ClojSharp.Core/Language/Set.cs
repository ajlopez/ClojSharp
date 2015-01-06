﻿namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;

    public class Set : IMetadata
    {
        private HashSet<object> keys;
        private Set set;
        private Map metadata;

        public Set(IList<object> keys)
            : this(keys, null, null)
        {
        }

        internal Set(IList<object> keys, Set set, Map metadata)
        {
            this.metadata = metadata;
            this.set = set;

            if (keys == null)
                this.keys = new HashSet<object>();
            else
                this.keys = new HashSet<object>(keys);
        }

        public Map Metadata
        {
            get { return this.metadata; }
        }

        public static Set Create(IList<object> keys)
        {
            return new Set(keys);
        }

        public bool HasKey(object key)
        {
            if (this.keys.Contains(key))
                return true;

            if (this.set != null)
                return this.set.HasKey(key);

            return false;
        }

        public Set Add(object key)
        {
            return new Set(new object[] { key }, this, this.metadata);
        }
    }
}
