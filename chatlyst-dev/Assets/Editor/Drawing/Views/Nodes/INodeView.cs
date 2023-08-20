using Chatlyst.Runtime.Data;
using UnityEngine;

namespace Chatlyst.Editor
{
    /// <summary>
    ///     The node needed to be visualization
    /// </summary>
    interface INodeView
    {
        /// <summary>
        ///     Build a new node instance.
        /// </summary>
        /// <param name="pos">The position of the node.</param>
        public void BuildNewInstance(Rect pos);

        /// <summary>
        ///     Rebuild a node instance from old data.
        /// </summary>
        /// <param name="nodeData">Old data.</param>
        public void RebuildInstance(BasicNode nodeData);
    }
}
