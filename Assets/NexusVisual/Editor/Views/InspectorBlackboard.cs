using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace NexusVisual.Editor
{
    public class InspectorBlackboard : Blackboard
    {
        public void Inspector()
        {
            var nodes = graphView.selection;
            foreach (var node in nodes)
            {
                if (node is DialogueNode dialogueNode)
                {
                    title = dialogueNode.title;
                }
            }
        }
    }
}