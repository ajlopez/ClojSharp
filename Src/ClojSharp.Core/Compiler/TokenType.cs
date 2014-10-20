namespace ClojSharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum TokenType
    {
        Name = 1,
        Integer = 2,
        Real = 3,
        String = 4,
        Character = 5,
        Separator = 6,
        Keyword = 7
    }
}
