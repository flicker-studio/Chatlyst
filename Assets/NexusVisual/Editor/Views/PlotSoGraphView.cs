using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    public class PlotSoGraphView : GraphView
    {
        private readonly InspectorBlackboard _inspector = new InspectorBlackboard();

        public Action OnUpdate;

        public class Factory : UxmlFactory<PlotSoGraphView, UxmlTraits>
        {
        }


        public PlotSoGraphView()
        {
            Add(_inspector);
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            OnUpdate += InspectorNode;
        }

        private void InspectorNode()
        {
            if (selection.Count > 0) _inspector.Inspector(selection[0]);
        }

        public override Blackboard GetBlackboard() => _inspector;

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

        ~PlotSoGraphView()
        {
            OnUpdate -= InspectorNode;
        }
    }
}