using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AVG.Runtime.ExtensionMethod
{
    public static class VectorExtension
    {
        public static Rect ToNodePosition(this Vector2 current, GraphView graphView) =>
            new Rect(
                position: new Vector2
                              (graphView.viewTransform.position.x, graphView.viewTransform.position.y) *
                          -(1 / graphView.scale) +
                          current * (1 / graphView.scale), Vector2.one);
    }
}