using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public class PlotVisualElement : VisualElement
    {
        public PlotVisualElement(SectionNode sectionNode, VisualTreeAsset uxml)
        {
            var node1 = sectionNode;
            uxml.CloneTree(this);

            TextField characterName = this.Query<TextField>("CharacterName");

            characterName.value = node1.SectionData.characterName;
            characterName.RegisterValueChangedCallback(
                e => { node1.SectionData.characterName = characterName.value; }
            );

            TextField dialogueText = this.Query<TextField>("DialogueText");
            dialogueText.value = node1.SectionData.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                e => { node1.SectionData.dialogueText = dialogueText.value; }
            );
        }
    }
}