using System;

namespace Chatlyst.Editor.Attribute
{
    /// <summary>
    ///     Simple configuration name of SearchTree item.
    /// </summary>
    /// <example>
    ///     For example, item will show "Start" by using <c>[SearchTreeName("Start")]</c>.
    /// </example>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class SearchTreeNameAttribute : System.Attribute
    {
        /// <summary>
        ///     The name will be showed.
        /// </summary>
        public readonly string Name;

        /// <summary>
        ///     <seealso cref="SearchTreeNameAttribute" />
        /// </summary>
        /// <param name="name">The name will be showed.</param>
        public SearchTreeNameAttribute(string name)
        {
            Name = name;
        }
    }
}
