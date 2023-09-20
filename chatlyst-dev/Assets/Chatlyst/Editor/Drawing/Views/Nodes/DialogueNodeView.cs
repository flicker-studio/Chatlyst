using System;
using Chatlyst.Editor.Attribute;
using Chatlyst.Runtime.Data;
using UnityEngine;

namespace Chatlyst.Editor
{
    [SearchTreeName("对话节点")] [NodePort(1, 1)]
    public class DialogueNodeView : NodeView
    {
        public DialogueNodeView() : base("UXML/Start") { }

        public override void BuildNewInstance(Rect pos)
        {
        }

        public override
            void RebuildInstance(BasicNode nodeData)
        {
        }

        public override void RefreshData()
        {
            throw new NotImplementedException();
        }
    }
}
