using NexusVisual.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NexusVisual.Editor
{
    internal sealed class StartNode : BaseNvNode<StartNvData>, IVisible
    {
        public StartNode(StartNvData nodeNvData = null, Rect targetPos = new Rect())
        {
            visualTree = CustomSettingProvider.GetSettings().nodeSetting.startNode;
            Construction(nodeNvData, targetPos);
        }

        public StartNode()
        {
            visualTree = CustomSettingProvider.GetSettings().nodeSetting.startNode;
            Construction(null, targetPos: new Rect());
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