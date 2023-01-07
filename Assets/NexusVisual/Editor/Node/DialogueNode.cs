using System.Linq;
using NexusVisual.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    internal sealed class DialogueNode : BaseNvNode<DialogueData>, IVisible
    {
        public DialogueNode(DialogueData nodeData = null, Rect targetPos = new Rect())
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
            var data = (DialogueData)userData;
            var a = data.dialogueList.Aggregate<Dialogue, string>(null, (current, dialogue) =>
                current + (dialogue.ToSentence() + "\n"));
            mainContainer.Q<Label>("Lables").text = a;
        }
    }
}