using System.Collections.Generic;
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


        public Rect ToNodePosition(Vector2 current) =>
            new((new Vector2(viewTransform.position.x, viewTransform.position.y) * -(1 / scale)) +
                (current * (1 / scale)), Vector2.one);
    }
}