using System.Collections.Generic;
using AVG.Runtime.Plot;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public class PlotGraphView : GraphView
    {
        public PlotGraphView()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }

        public void AddNode(Vector2 mousePos)
        {
            var node = new SectionNode();
            node.SetPosition(new Rect(
                (new Vector2(viewTransform.position.x, viewTransform.position.y) * -(1 / scale)) +
                (mousePos * (1 / scale)), Vector2.one));
            node.mainContainer.Add(node.PlotVisualElement);


            var inputPort =
                node.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            var outputPort =
                node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = "Next";
            node.outputContainer.Add(outputPort);

            node.RefreshExpandedState();
            node.RefreshPorts();
            AddElement(node);
        }

        public void RedrawNode(SectionData data)
        {
            var node = new SectionNode();
            node.SetPosition(new Rect(
                (new Vector2(viewTransform.position.x, viewTransform.position.y) * -(1 / scale)) +
                (data.nodePos.position * (1 / scale)), Vector2.one));
            node.SectionData = data;
            node.mainContainer.Add(node.PlotVisualElement);


            var inputPort =
                node.InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(float));
            inputPort.portName = "Input";
            node.inputContainer.Add(inputPort);

            var outputPort =
                node.InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(float));
            outputPort.portName = "Next";
            node.outputContainer.Add(outputPort);

            node.RefreshExpandedState();
            node.RefreshPorts();
            AddElement(node);
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