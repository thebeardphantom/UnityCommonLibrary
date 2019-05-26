using System;

namespace BeardPhantom.UCL.Attributes
{
    /// <summary>
    /// For marking a field to be injected automatically
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class InjectAttribute : Attribute { }
}