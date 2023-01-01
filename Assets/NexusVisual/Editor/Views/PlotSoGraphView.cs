using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    public class PlotSoGraphView : GraphView
    {
        public readonly Blackboard Inspector = new InspectorBlackboard();

        public class Factory : UxmlFactory<PlotSoGraphView, UxmlTraits>
        {
        }

        public PlotSoGraphView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            Add(Inspector);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort != port && startPort.node != port.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }
    }
}