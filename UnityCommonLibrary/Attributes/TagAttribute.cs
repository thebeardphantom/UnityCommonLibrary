using System;
using UnityEngine;

namespace BeardPhantom.UCL.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TagAttribute : PropertyAttribute { }
}