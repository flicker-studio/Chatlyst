using AVG.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace AVG.Editor
{
    internal sealed class DialogueNode : GraphNode<DialogueSection>
    {
        public DialogueNode(DialogueSection section = null)
        {
            Section = section ?? new DialogueSection();
            var visualAsset = EditorGUIUtility.Load("GraphNode.uxml") as VisualTreeAsset;
            VisualElement = CreatVisual(visualAsset);
        }

        private protected override VisualElement CreatVisual(VisualTreeAsset uxml)
        {
            var visualElement = base.CreatVisual(uxml);
            Foldout foldout = visualElement.Query<Foldout>("Fold");
            Button addButton = visualElement.Query<Button>("Add");
            VisualElement dialogue = visualElement.Query<VisualElement>("Base");
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
            foldout.Add(dialogue);
            addButton.clicked += () =>
            {
                var element = new VisualElement();
                element.Add(new TextField("Name"));
                element.Add(new TextField("Dialogue"));
                foldout.Add(element);
            };
            return visualElement;
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