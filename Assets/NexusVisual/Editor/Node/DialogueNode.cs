using System;
using System.Collections.Generic;
using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    internal sealed class DialogueNode : BaseNvNode<DialogueSection>, IVisible
    {
        public DialogueNode(DialogueSection nodeData = null, Rect targetPos = new Rect())
        {
            visualTree = CustomSettingProvider.GetSettings().nodeSetting.dialogueNode;
            Construction(nodeData, targetPos);
        }

        private protected override void Visualization()
        {
            base.Visualization();
            title = "Dialogue Group";
            var inputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "Input";
            inputContainer.Add(inputPort);

            var outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
                typeof(float));
            outputPort.portName = "Next";
            outputContainer.Add(outputPort);
        }

        private protected override void DataBind()
        {
            //Serialized object bind
            mainElement.Q<TextField>("CharacterName").BindProperty(serializedObject.FindProperty("characterName"));
            //   mainElement.Q<TextField>("DialogueText").BindProperty(serializedObject.FindProperty("dialogueText"));
            var basicSentence = mainElement.Q<VisualElement>("Sentence");
            var data = userData as DialogueSection;


            if (!data) throw new Exception("Can't bind dialogue node");

            var a = new Dialogue
            {
                text = "?",
                name = "?"
            };
            data.dialogues.Add(a);

            basicSentence.Q<Label>("Text").text = data.dialogues[0].text;


            /*
            Foldout foldout = this.Query<Foldout>("Fold");
            Button addButton = this.Query<Button>("Add");
            VisualElement dialogue = this.Query<VisualElement>("Base");

            characterNameText.BindProperty(s.FindProperty("characterName"));
            dialogueText.BindProperty(s.FindProperty("dialogueText"));
            characterName.value = data?.characterName;
            characterName.RegisterValueChangedCallback(
                _ => { data.characterName = characterName.value; }
            );
            dialogueText.value = data?.dialogueText;
            dialogueText.RegisterValueChangedCallback(
                _ => { data.dialogueText = dialogueText.value; }
            );

            foldout.Add(dialogue);

            addButton.clicked += () =>
            {
                var element = new VisualElement();
                element.Add(new TextField("Name"));
                element.Add(new TextField("Dialogue"));
                foldout.Add(element);
            };
            */
        }
    }
}