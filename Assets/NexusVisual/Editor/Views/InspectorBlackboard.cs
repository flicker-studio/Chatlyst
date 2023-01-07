using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

namespace NexusVisual.Editor
{
    public sealed class InspectorBlackboard : Blackboard
    {
        private readonly PropertyField _inspector = new PropertyField();
        private ISelectable _currentNode;

        public InspectorBlackboard()
        {
            contentContainer.Add(_inspector);
        }

        public void Inspector(ISelectable target)
        {
            if (_currentNode == target) return;
            _currentNode = target;

            if (_currentNode is DialogueNode dialogueNode)
            {
                _inspector.BindProperty(dialogueNode.serializedObject.FindProperty("dialogueList"));
            }
        }
    }
}