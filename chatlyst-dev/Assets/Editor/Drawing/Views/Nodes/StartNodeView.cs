using Chatlyst.Runtime;
using UnityEngine;

namespace Chatlyst.Editor
{
    [SearchTreeName("开始节点"), NodePort(0, 1)]
    public sealed class StartNodeView : NodeView, IVisible
    {
        private const string    UxmlPath = "UXML/Start";
        private       BeginNode _node;

        public StartNodeView()
        {
        }

        public void CreateNewInstance(Rect pos)
        {
            
        }

        public void RebuildInstance(BasicNode nodeData)
        {
        }
    }
}
