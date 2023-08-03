using System;
using System.Collections.Generic;
using Chatlyst.Runtime.Util;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Chatlyst.Runtime
{
    public class NodeIndex
    {
        [JsonProperty]
        private readonly string _id;

        public List<BeginNode> BeginNodes = new List<BeginNode>();

        public NodeIndex()
        {
            _id = Guid.NewGuid().ToString();
        }

        /// <summary>
        ///     Add a node to nodes list
        /// </summary>
        /// <param name="node">A node</param>
        /// <exception cref="ArgumentOutOfRangeException">Unknown Node type</exception>
        public void AutoAddNode(BasicNode node)
        {
            switch (node.NodeType)
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
                    BeginNodes.Add((BeginNode)node);
                    break;
                case NodeType.BRA:
                    break;
                case NodeType.END:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AutoAddNodes(List<BasicNode> nodes)
        {
            foreach (var basicNode in nodes)
            {
                AutoAddNode(basicNode);
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        ///     Deserialize data from Json
        /// </summary>
        /// <param name="json">Enter Json</param>
        /// <returns>Instance</returns>
        [CanBeNull]
        public static NodeIndex DeserializeFromJson(string json)
        {
            return JsonConvert.DeserializeObject<NodeIndex>(json);
        }

        private bool NodeListEquals(NodeIndex other)
        {
            return other.BeginNodes.AreSimilar(BeginNodes);
        }

        public override bool Equals(object obj)
        {
            return obj is NodeIndex index && NodeListEquals(index);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}
