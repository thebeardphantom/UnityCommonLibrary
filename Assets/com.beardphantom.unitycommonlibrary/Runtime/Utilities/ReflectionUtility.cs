using System;
using System.Reflection;

public static class ReflectionUtility
{
    #region Methods

    public static FieldInfo GetFirstFieldInTypeChain(string name, Type type, BindingFlags flags)
    {
        var inspectedType = type;
        while (inspectedType != null)
        {
            var field = inspectedType.GetField(name, flags);
            if (field != null)
            {
                return field;
            }

            inspectedType = inspectedType.BaseType;
        }

        return null;
    }

    #endregion
}