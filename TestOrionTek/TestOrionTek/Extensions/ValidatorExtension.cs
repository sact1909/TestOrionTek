using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TestOrionTek.Extensions
{
    public static class ValidatorExtension
    {
        public static bool HasAPropertyEmpty<TSource>(this TSource source) where TSource : class
        {
            Type type = source.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(source);

                if (value == null || string.IsNullOrEmpty(value.ToString()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
