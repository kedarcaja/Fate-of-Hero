using System.Collections.Generic;
using System.Reflection;

namespace System.Extensions
{
    public static class SystemExtensions
    {
        public static object GetPropertyValueByName(this System.Object obj, string name)
        {
            PropertyInfo p = obj.GetType().GetProperty(name);
            return Convert.ChangeType(p.GetValue(obj), p.PropertyType);
        }
        public static void SetPropertyValueByName(this System.Object obj, string name, object value)
        {
           PropertyInfo p = obj.GetType().GetProperty(name);
           p.SetValue(obj, Convert.ChangeType(value, p.PropertyType));
        }

        /// <summary>
        /// Compares all properties on object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static bool EqualsInstanceProperties<T>(this System.Object self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return self == to;
        }
    }
}
