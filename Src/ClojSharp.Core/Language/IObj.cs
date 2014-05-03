namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IObj : IMeta
    {
        IObj WithMeta(Map map);
    }
}
