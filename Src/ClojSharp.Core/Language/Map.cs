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

            return null;
        }
    }
}
