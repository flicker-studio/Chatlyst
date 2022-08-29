using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace AVG.Editor.Plot_Visual
{
    public sealed class DialogueNode : GraphNode //<DialogueSection>
    {
        public readonly DialogueSection Section;

        public DialogueNode(DialogueSection baseSection = null)
        {
            Section = baseSection ?? new DialogueSection();
            Guid = Section.guid;
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

        public override void NodeVisual()
        {
            var inputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "Input";
            inputContainer.Add(inputPort);

            var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(float));
            outputPort.portName = "Next";
            outputContainer.Add(outputPort);

            RefreshExpandedState();
            RefreshPorts();
        }
    }
}