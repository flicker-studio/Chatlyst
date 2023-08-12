using Chatlyst.Editor.Data;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime;
using UnityEngine;

namespace Chatlyst.Editor
{
    [SearchTreeName("对话节点"), NodePort(1, 1)]
    public class DialogueNodeView : NodeView, IVisible
    {
        private const string UxmlPath = "UXML/Start";

        public DialoguesNode _node;

        public void CreateInstance(Rect pos)
        {
            _node = new DialoguesNode();
            SetPosition(pos);
        }

        public void CreateNewInstance(Rect pos)
        {
           
        }

        public void RebuildInstance(BasicNode nodeData)
        {
            
        }

        public override void RefreshData()
        {
            throw new System.NotImplementedException();
        }
    }
}
