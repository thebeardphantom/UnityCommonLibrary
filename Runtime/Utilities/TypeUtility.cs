using System;
using System.Linq;
using System.Reflection;

namespace BeardPhantom.UCL.Utility
{
    public static class TypeUtility
    {
        #region Methods

        public static Type[] GetTypesSafe(this Assembly asm)
        {
            Type[] types;
            try
            {
                types = asm.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                types = e.Types.Where(t => t != null).ToArray();
            }

            return types;
        }

        public static bool InheritsFrom(this Type subtype, Type parentType)
        {
            return parentType.IsAssignableFrom(subtype);
        }

        public static T GetCustomAttribute<T>(this Type type, bool inherit)
            where T : Attribute
        {
            var attributes = type.GetCustomAttributes(typeof(T), inherit);

            if (attributes.Length > 0)
            {
                return (T) attributes[0];
            }

            return null;
        }

        public static T[] GetCustomAttributes<T>(this Type type, bool inherit)
            where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), inherit)
                .Cast<T>()
                .ToArray();
        }

        #endregion
    }
}