using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

//TODO:make dialogue list be reorderable
namespace NexusVisual.Editor
{
    public sealed class InspectorBlackboard : Blackboard
    {
        private ISelectable _currentNode;
        private readonly ListView _inspector = new ListView();


        public InspectorBlackboard()
        {
            var mItemAsset = CustomSettingProvider.GetSettings().nodeSetting.dialogueInspector;
            _inspector.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            _inspector.reorderMode = ListViewReorderMode.Simple;
            _inspector.showAddRemoveFooter = true;
            _inspector.showBorder = true;
            _inspector.showFoldoutHeader = true;
            _inspector.makeItem = mItemAsset.CloneTree;
            contentContainer.Add(_inspector);
        }

        public void Inspector(ISelectable target)
        {
            if (_currentNode == target) return;
            _currentNode = target;

            switch (_currentNode)
            {
                case DialogueNode dialogueNode:
                    _inspector.BindProperty(dialogueNode.serializedObject.FindProperty("dialogueList"));
                    break;
            }
        }
    }
}