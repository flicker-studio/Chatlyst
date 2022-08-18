using AVG.Runtime.VisualGraph;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AVG.Editor.VisualGraph
{
    public class NodeVisual : Node
    {
        public VisualTreeAsset VisualAsset;
        public GraphNode GraphNode;
        public NodeViewer NodeViewer;

        public NodeVisual(GraphNode baseData = null)
        {
            GraphNode = baseData ?? new GraphNode();
            VisualAsset = EditorGUIUtility.Load("GraphNode.uxml") as VisualTreeAsset;
            NodeViewer = new NodeViewer(this, VisualAsset);
        }
    }
}