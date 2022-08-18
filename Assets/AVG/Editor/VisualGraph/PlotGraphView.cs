using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AVG.Editor.VisualGraph
{
    public class PlotGraphView : GraphView
    {
        public PlotGraphView()
        {
            SetupZoom(0.1f, 2);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }

        public void CreatNode()
        {
            var node = new NodeVisual();

            node.mainContainer.Add(node.NodeViewer);
            node.RefreshExpandedState();
            node.RefreshPorts();
            AddElement(node);
        }
    }
}