using System;
using Chatlyst.Runtime;
using Chatlyst.Runtime.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace Chatlyst.Editor
{
    public static class NodeViewFactory
    {
        [CanBeNull]
        public static NodeView CreatNewNodeView(NodeType type, BasicNode data = null, Rect rect = new Rect())

        {
            if (data != null)
            {
                if (type != data.NodeType)
                {
                    throw new Exception("The types do not match!");
                }
                switch (type)
                {
                    case NodeType.NDEF:
                        break;
                    case NodeType.DIA:
                        break;
                    case NodeType.VFX:
                        break;
                    case NodeType.CUT:
                        break;
                    case NodeType.BEG:
                        var a = new BeginNodeView();
                        a.RebuildInstance(data);
                        return a;
                    case NodeType.BRA:
                        break;
                    case NodeType.END:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            else
            {
                switch (type)
                {
                    case NodeType.NDEF:
                        break;
                    case NodeType.DIA:
                        break;
                    case NodeType.VFX:
                        break;
                    case NodeType.CUT:
                        break;
                    case NodeType.BEG:
                        var a = new BeginNodeView();
                        a.BuildNewInstance(rect);
                        return a;
                    case NodeType.BRA:
                        break;
                    case NodeType.END:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            return null;
        }
    }
}
