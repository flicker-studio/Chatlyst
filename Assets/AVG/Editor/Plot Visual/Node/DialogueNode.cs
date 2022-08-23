using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace AVG.Editor.Plot_Visual
{
    public class DialogueNode : Node, INode<DialogueSection>
    {
        public DialogueSection DialogueSection;
        public VisualElement visual { get; set; }

        /// public readonly PlotVisualElement PlotVisualElement;
        public DialogueNode(DialogueSection baseSection = null)
        {
            DialogueSection = baseSection ?? new DialogueSection();
            var visualAsset = EditorGUIUtility.Load("GraphNode.uxml") as VisualTreeAsset;
            visual = CreatVisual(visualAsset);
        }

        public VisualElement CreatVisual(VisualTreeAsset uxml)
        {
            var visualElement = new VisualElement();
            uxml.CloneTree(visualElement);

            TextField characterName = visualElement.Query<TextField>("CharacterName");

            characterName.value = DialogueSection.characterName;
            characterName.RegisterValueChangedCallback(
                _ => { DialogueSection.characterName = characterName.value; }
            );

            TextField dialogueText = visualElement.Query<TextField>("DialogueText");
            dialogueText.value = DialogueSection.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                _ => { DialogueSection.dialogueText = dialogueText.value; }
            );
            return visualElement;
        }
    }
}