namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ISeq
    {
        object First { get; }

        ISeq Next { get; }

        ISeq Rest { get; }
    }
}
