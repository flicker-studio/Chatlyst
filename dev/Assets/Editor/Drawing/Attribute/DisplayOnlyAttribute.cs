using System;
using UnityEngine;

namespace NexusVisual.Editor
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class DisplayOnlyAttribute : PropertyAttribute
    {
    }
}