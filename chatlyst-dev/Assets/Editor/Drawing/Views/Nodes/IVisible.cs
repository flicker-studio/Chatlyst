using Chatlyst.Runtime;
using UnityEngine;

namespace Chatlyst.Editor
{
    /// <summary>
    ///     The node needed to be visualization
    /// </summary>
    interface IVisible
    {
        public void CreateNewInstance(Rect pos);
        public void RebuildInstance(BasicNode nodeData);
    }
}
