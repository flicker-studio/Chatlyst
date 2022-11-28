using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace NexusVisual.Editor
{
    internal sealed class DialogueNode : BaseNvNode<DialogueSection>, IVisible
    {
        public DialogueNode(DialogueSection nodeData = null, Rect targetPos = new Rect())
        {
            uxmlPath = "BaseNvNode.uxml";
            Construction(nodeData, targetPos);
        }
        
        private protected override void Visualization()
        {
            base.Visualization();
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

        private protected override void DataBind()
        {
            Foldout foldout = visualElement.Query<Foldout>("Fold");
            Button addButton = visualElement.Query<Button>("Add");
            VisualElement dialogue = visualElement.Query<VisualElement>("Base");
            TextField characterName = visualElement.Query<TextField>("CharacterName");
            TextField dialogueText = visualElement.Query<TextField>("DialogueText");

            //Serialized object bind
            var s = new SerializedObject(data);
            characterName.BindProperty(s.FindProperty("characterName"));
            dialogueText.BindProperty(s.FindProperty("dialogueText"));
            /*  characterName.value = data?.characterName;
              characterName.RegisterValueChangedCallback(
                  _ => { data.characterName = characterName.value; }
              );
            dialogueText.value = data?.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                _ => { data.dialogueText = dialogueText.value; }
            );
             */
            foldout.Add(dialogue);
            addButton.clicked += () =>
            {
                var element = new VisualElement();
                element.Add(new TextField("Name"));
                element.Add(new TextField("Dialogue"));
                foldout.Add(element);
            };
        }
    }
}