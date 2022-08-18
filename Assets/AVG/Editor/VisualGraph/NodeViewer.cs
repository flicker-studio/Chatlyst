using UnityEngine.UIElements;

namespace AVG.Editor.VisualGraph
{
    public class NodeViewer : VisualElement
    {
        public NodeViewer(NodeVisual node, VisualTreeAsset uxml)
        {
            var node1 = node;
            // uxml.Instantiate();
            uxml.CloneTree(this);

            TextField characterName = this.Query<TextField>("CharacterName");

            characterName.value = node1.GraphNode.characterName;
            characterName.RegisterValueChangedCallback(
                e => { node1.GraphNode.characterName = characterName.value; }
            );

            TextField dialogueText = this.Query<TextField>("DialogueText");
            dialogueText.value = node1.GraphNode.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                e => { node1.GraphNode.dialogueText = dialogueText.value; }
            );
        }
    }
}