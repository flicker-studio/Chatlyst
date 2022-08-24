using System.Collections.Generic;
using AVG.Runtime.PlotTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public class PlotGraphView : GraphView
    {
        public PlotGraphView()
        {
            SetupZoom(.01f, 5f);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }

        public void AddNode(Vector2 mousePos)
        {
            var node = new DialogueNode();
            node.SetPosition(new Rect(
                (new Vector2(viewTransform.position.x, viewTransform.position.y) * -(1 / scale)) +
                (mousePos * (1 / scale)), Vector2.one));
            node.mainContainer.Add(node.VisualElement);


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

        public void RedrawNode(DialogueSection section)
        {
            var node = new DialogueNode(section);
            var pos = section.nodePos;
            node.SetPosition(new Rect(
                (new Vector2(viewTransform.position.x, viewTransform.position.y) * -(1 / scale)) +
                (pos.position * (1 / scale)), Vector2.one));

            node.mainContainer.Add(node.VisualElement);


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