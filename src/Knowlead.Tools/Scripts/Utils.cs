using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Knowlead.Tools
{
    public static class Utils
    {
        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                var type = typeof(T);
                var ignoreList = new List<string>(ignore);
                var unequalProperties =
                    from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where
                        !ignoreList.Contains(pi.Name) &&
                        pi.GetCustomAttribute(typeof(KeyAttribute)) == null &&
                        IsSimple(pi.PropertyType)
                    let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
                    let toValue = type.GetProperty(pi.Name).GetValue(to, null)
                    where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
                    select selfValue;
                return !unequalProperties.Any();
            }
            return self == to;
        }
        public static bool IsSimple(Type type)
        {
            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.GetTypeInfo().IsPrimitive 
                || type.GetTypeInfo().IsEnum
                || type.Equals(typeof(string))
                || type.Equals(typeof(decimal));
        }
    }
}