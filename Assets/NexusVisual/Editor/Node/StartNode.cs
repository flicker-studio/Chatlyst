using NexusVisual.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NexusVisual.Editor
{
    internal sealed class StartNode : BaseNvNode<StartSection>, IVisible
    {
        public StartNode(StartSection nodeData = null, Rect targetPos = new Rect())
        {
            visualTree = CustomSettingProvider.GetSettings().nodeSetting.startNode;
            Construction(nodeData, targetPos);
        }

        private protected override void DataBind()
        {
        }

        private protected override void Visualization()
        {
            base.Visualization();
            title = "Start";
            var outputPort =
                InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = "Next";
            outputContainer.Add(outputPort);
        }
    }
}