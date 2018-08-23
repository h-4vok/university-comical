using System;
namespace System
{
    public static class ObjectExtensions
    {
        public static bool AsBool(this object obj)
        {
            return Convert.ToBoolean(obj);
        }

        public static string AsString(this object obj)
        {
            return String.Format("{0}", obj);
        }

        public static int? AsIntNullable(this object obj)
        {
            if (obj == null)
                return null;

            return obj.AsInt();
        }

        public static int AsInt(this object obj)
        {
            return Convert.ToInt32(obj);
        }

        public static double AsDouble(this object obj)
        {
            return Convert.ToDouble(obj);
        }

        public static object GetValueOfProperty(this object obj, string propertyName)
        {
            var type = obj.GetType();
            var property = type.GetProperty(propertyName);
            var value = property.GetValue(obj);

            return value;
        }
    }
}
