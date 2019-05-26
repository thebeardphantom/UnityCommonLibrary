using System;

namespace BeardPhantom.UCL.Attributes
{
    /// <summary>
    /// Displays a ScriptableObject for selection in the CustomAssetCreateMenu
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAssetCreateMenuAttribute : Attribute { }
}