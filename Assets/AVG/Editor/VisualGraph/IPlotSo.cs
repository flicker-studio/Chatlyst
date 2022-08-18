using AVG.Runtime.VisualGraph;

namespace AVG.Editor.VisualGraph
{
    public interface IPlotSo
    {
        public GraphNode GetStartNode();
        public GraphNode GetCurrentNode(string nodeGuid);
        public GraphNode GetNextNode(string nodeGuid);
    }
}