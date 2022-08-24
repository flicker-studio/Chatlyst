using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace AVG.Editor.Plot_Visual
{
    public class DialogueNode : GraphNode<DialogueSection>
    {
        public readonly DialogueSection DialogueSection;
        public readonly VisualElement VisualElement;

        public DialogueNode(DialogueSection baseSection = null)
        {
            DialogueSection = baseSection ?? new DialogueSection();
            var visualAsset = EditorGUIUtility.Load("GraphNode.uxml") as VisualTreeAsset;
            VisualElement = CreatVisual(visualAsset);
        }

        protected override VisualElement CreatVisual(VisualTreeAsset uxml)
        {
            var visualElement = base.CreatVisual(uxml);

            TextField characterName = visualElement.Query<TextField>("CharacterName");

            characterName.value = DialogueSection?.characterName;
            characterName.RegisterValueChangedCallback(
                _ => { DialogueSection.characterName = characterName.value; }
            );

            TextField dialogueText = visualElement.Query<TextField>("DialogueText");
            dialogueText.value = DialogueSection?.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                _ => { DialogueSection.dialogueText = dialogueText.value; }
            );
            return visualElement;
        }
    }
}