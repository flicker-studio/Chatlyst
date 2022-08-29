using UnityEngine;

namespace AVG.Editor.Plot_Visual
{
    public interface IPlotGraphView
    {
        public void AddStartNode(Vector2 mousePos);
        public void AddDialogueNode(Vector2 mousePos);
    }
}