using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

//TODO:make dialogue list be reorderable
namespace Chatlyst.Editor
{
    public sealed class InspectorBlackboard : Blackboard
    {
        private          DialogueNodeView _currentNodeView;
        private readonly ListView         _inspector = new ListView();

        public InspectorBlackboard()
        {
            var mItemAsset = CustomSettingProvider.GetSettings().nodeSetting.dialogueInspector;
            _inspector.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
            _inspector.reorderMode          = ListViewReorderMode.Simple;
            _inspector.showAddRemoveFooter  = true;
            _inspector.showBorder           = true;
            _inspector.showFoldoutHeader    = true;
            _inspector.makeItem             = mItemAsset.CloneTree;
            contentContainer.Add(_inspector);
        }

        public void Inspector(ISelectable target)
        {
            if (target == null)
            {
                return;
            }

            if (target is not DialogueNodeView node)
            {
                _inspector.visible = false;
                return;
            }

            _inspector.visible = true;
            _currentNodeView       = node;
            _inspector.BindProperty(_currentNodeView._node.getListProperty);
        }
    }
}
