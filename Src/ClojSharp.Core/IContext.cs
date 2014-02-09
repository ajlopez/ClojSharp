namespace ClojSharp.Core
{
    using System;

    public interface IContext
    {
        VarContext TopContext { get; }

        object GetValue(string name);

        void SetValue(string name, object value);
    }
}
