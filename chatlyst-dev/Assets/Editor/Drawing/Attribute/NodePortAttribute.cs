using System;

namespace Chatlyst.Editor.Attribute
{
    /// <summary>
    ///     Simple configuration of the number of ports
    /// </summary>
    /// <example>
    ///     For example, there are three input ports and two out ports by using <c>[NodePort(3, 2)]</c>.
    /// </example>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NodePortAttribute : System.Attribute
    {
        /// <summary>
        ///     Number of input ports.
        /// </summary>
        public readonly int InputPortNum;
        /// <summary>
        ///     Number of out ports.
        /// </summary>
        public readonly int OutputPortNum;

        /// <summary>
        ///     <seealso cref="NodePortAttribute" />
        /// </summary>
        /// <param name="inNum">Number of input ports.</param>
        /// <param name="outNum">Number of out ports.</param>
        public NodePortAttribute(int inNum, int outNum)
        {
            InputPortNum  = inNum;
            OutputPortNum = outNum;
        }
    }
}
