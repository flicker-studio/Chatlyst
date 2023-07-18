using System;
using UnityEngine;

namespace Chatlyst.Editor
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    public class DisplayOnlyAttribute : PropertyAttribute
    {
    }
}