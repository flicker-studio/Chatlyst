using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace AVG.Editor.Plot_Visual
{
    public sealed class DialogueNode : GraphNode<DialogueSection>
    {
        public DialogueNode()
        {
            //Section = baseSection ?? new DialogueSection();
            var visualAsset = EditorGUIUtility.Load("GraphNode.uxml") as VisualTreeAsset;
            VisualElement = CreatVisual(visualAsset);
        }

        protected override VisualElement CreatVisual(VisualTreeAsset uxml)
        {
            var visualElement = base.CreatVisual(uxml);

            TextField characterName = visualElement.Query<TextField>("CharacterName");

            characterName.value = Section?.characterName;
            characterName.RegisterValueChangedCallback(
                _ => { Section.characterName = characterName.value; }
            );

            TextField dialogueText = visualElement.Query<TextField>("DialogueText");
            dialogueText.value = Section?.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                _ => { Section.dialogueText = dialogueText.value; }
            );
            return visualElement;
        }
    }
}