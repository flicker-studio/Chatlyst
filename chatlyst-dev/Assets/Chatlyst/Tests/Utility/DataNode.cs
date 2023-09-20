using System.Collections.Generic;
using Chatlyst.Runtime;

namespace Tests.Utility
{
    /// <summary>
    ///     Used to quickly get a variety of DataNode
    /// </summary>
    public static class DataNode
    {
        /// <summary>
        ///     Gets a randomly generated <see cref="BeginNode" />
        /// </summary>
        /// <returns> A <see cref="BeginNode" /> with random label and number.</returns>
        public static BeginNode GetBeginNode()
        {
            return new BeginNode(Utilities.RandomString(5), Utilities.RandomInt());
        }

        /// <summary>
        ///     Gets a randomly generated List of <see cref="BeginNode" />
        /// </summary>
        /// <param name="length">The length of desired List</param>
        /// <returns>A List</returns>
        public static List<BeginNode> GetBeginNodeList(int length)
        {
            var list = new List<BeginNode>();
            for (int i = 0; i < length; i++)
            {
                list.Add(GetBeginNode());
            }
            return list;
        }
    }
}
