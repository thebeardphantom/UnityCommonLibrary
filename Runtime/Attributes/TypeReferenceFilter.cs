using System;

namespace BeardPhantom.UCL.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TypeReferenceFilterAttribute : Attribute
    {
        #region Fields

        public readonly Type Type;

        #endregion

        #region Constructors

        public TypeReferenceFilterAttribute(Type type)
        {
            Type = type;
        }

        #endregion
    }
}