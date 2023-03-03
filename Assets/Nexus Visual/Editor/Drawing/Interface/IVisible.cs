using NexusVisual.Editor.Serialization;
using UnityEngine;

namespace NexusVisual.Editor
{
    ///<summary>
    /// The node needed to be visualization
    /// </summary>
    internal interface IVisible
    {
        public void CreateInstance(Rect pos);
        public void RebuildInstance(NexusJsonEntity entity);
        
    }
}