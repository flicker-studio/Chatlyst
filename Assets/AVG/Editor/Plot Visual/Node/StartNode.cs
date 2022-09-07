using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    internal sealed class StartNode : GraphNode<StartSection>
    {
        public StartNode(StartSection section = null)
        {
            Section = section ?? new StartSection();
            var visualAsset = EditorGUIUtility.Load("StartNode.uxml") as VisualTreeAsset;
            VisualElement = CreatVisual(visualAsset);
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