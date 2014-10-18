namespace ClojSharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Predicates
    {
        public static bool Equals(object obj1, object obj2)
        {
            if (obj1 == null)
                return obj2 == null;

            if (IsReal(obj1) && IsNumeric(obj2) && !IsReal(obj2))
                return Convert.ToDouble(obj1) == Convert.ToDouble(obj2);

            if (IsReal(obj2) && IsNumeric(obj1) && !IsReal(obj1))
                return Convert.ToDouble(obj1) == Convert.ToDouble(obj2);

            return obj1.Equals(obj2);
        }

        public static bool IsNil(object obj)
        {
            return obj == null;
        }

        public static bool IsZero(object obj)
        {
            return 0.Equals(obj) || 0.0.Equals(obj) || ((float)0.0).Equals(obj) || ((short)0).Equals(obj) || ((long)0).Equals(obj) || ((uint)0).Equals(obj) || ((ushort)0).Equals(obj) || ((ulong)0).Equals(obj);
        }

        public static bool IsFalse(object obj)
        {
            return obj == null || obj.Equals(false);
        }

        public static bool IsTrue(object obj)
        {
            return !IsFalse(obj);
        }

        public static bool IsReal(object obj)
        {
            return obj is double || obj is float;
        }

        public static bool IsInteger(object obj)
        {
            return obj is short || obj is int || obj is long || obj is ushort || obj is uint || obj is ulong;
        }

        public static bool IsNumeric(object obj)
        {
            return IsReal(obj) || IsInteger(obj);
        }
    }
}
