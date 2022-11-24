using NexusVisual.Runtime;
using UnityEditor.Experimental.GraphView;

namespace NexusVisual.Editor
{
    internal sealed class StartNode : BaseNvNode<StartSection>, IVisible
    {
        private const string UxmlPath = "StartNode.uxml";

        public StartNode()
        {
            Section = new StartSection();
            CreatVisual(UxmlPath);
        }

        public StartNode(StartSection section)
        {
            Section = section;
            CreatVisual(UxmlPath);
        }

        protected override void NodeVisual()
        {
            var outputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = "Next";
            outputContainer.Add(outputPort);

            RefreshExpandedState();
            RefreshPorts();
        }
    }
}