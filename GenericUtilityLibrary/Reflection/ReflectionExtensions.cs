using System;
using System.Reflection;

namespace com.cagdaskorkut.utility.Reflection
{
    public static class ReflectionExtensions
    {
        public static object GetPropertyValue(this object instance, string propertyName)
        {
            Type type = instance.GetType();
            PropertyInfo info = type.GetProperty(propertyName);
            if (info == null)
                return null;
            object value = info.GetValue(instance, null);
            return value;
        }


        public static bool HasProperty(this object instance, string propertyName)
        {
            Type type = instance.GetType();

            PropertyInfo info = type.GetProperty(propertyName);
            return (info != null);
        }
    }
}