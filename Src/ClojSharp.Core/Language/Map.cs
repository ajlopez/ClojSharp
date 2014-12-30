namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ClojSharp.Core.Exceptions;

    public class Map : IMetadata
    {
        private IDictionary<object, object> keyvalues = new Dictionary<object, object>();
        private Map metadata;

        public Map(IList<object> keyvalues)
            : this(keyvalues, null)
        {
        }

        internal Map(IList<object> keyvalues, Map metadata)
        {
            this.metadata = metadata;

            if (keyvalues != null)
            {
                if (keyvalues.Count % 2 != 0)
                    throw new RuntimeException("Map must be created with an even number of values");

                for (int k = 0; k < keyvalues.Count; k += 2)
                    this.keyvalues[keyvalues[k]] = keyvalues[k + 1];
            }
        }

        public Map Metadata
        {
            get { return this.metadata; }
        }

        public object GetValue(object name)
        {
            if (this.keyvalues.ContainsKey(name))
                return this.keyvalues[name];

            if (this.metadata != null)
                return this.metadata.GetValue(name);

            return null;
        }

        public bool HasValue(object name)
        {
            if (this.keyvalues.ContainsKey(name))
                return true;

            if (this.metadata != null)
                return this.metadata.HasValue(name);

            return false;
        }

        public Map SetValue(object name, object value)
        {
            return new Map(new object[] { name, value }, this);
        }

        public Map Merge(Map map)
        {
            Map newmap = this;

            if (map.metadata != null)
                newmap = newmap.Merge(map.metadata);

            foreach (var key in map.keyvalues.Keys)
                newmap = newmap.SetValue(key, map.keyvalues[key]);

            return newmap;
        }
    }
}
