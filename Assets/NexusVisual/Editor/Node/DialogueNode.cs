using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace NexusVisual.Editor
{
    internal sealed class DialogueNode : BaseNvNode<DialogueSection>, IVisible
    {
        private const string UxmlPath = "BaseNvNode.uxml";

        public DialogueNode(DialogueSection section = null)
        {
            Section = section ?? new DialogueSection();
            CreatVisual(UxmlPath);
        }

        private protected override void CreatVisual(string uxmlPath)
        {
            Foldout foldout = VisualElement.Query<Foldout>("Fold");
            Button addButton = VisualElement.Query<Button>("Add");
            VisualElement dialogue = VisualElement.Query<VisualElement>("Base");
            TextField characterName = VisualElement.Query<TextField>("CharacterName");
            characterName.value = Section?.characterName;
            characterName.RegisterValueChangedCallback(
                _ => { Section.characterName = characterName.value; }
            );

            TextField dialogueText = VisualElement.Query<TextField>("DialogueText");
            dialogueText.value = Section?.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                _ => { Section.dialogueText = dialogueText.value; }
            );
            foldout.Add(dialogue);
            addButton.clicked += () =>
            {
                var element = new VisualElement();
                element.Add(new TextField("Name"));
                element.Add(new TextField("Dialogue"));
                foldout.Add(element);
            };
        }

        protected override void NodeVisual()
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