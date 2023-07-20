using System;

namespace Chatlyst.Editor
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SearchTreeNameAttribute : Attribute
    {
        public readonly string Name;

        public SearchTreeNameAttribute(string name)
        {
            Name = name;
        }
    }
}