using System;
using System.Collections.Generic;
using Chatlyst.Runtime.Data;
using Chatlyst.Runtime.Util;
using Newtonsoft.Json;

namespace Chatlyst.Runtime
{
    public class NodeIndex
    {
        [JsonProperty]
        private readonly string _id = Guid.NewGuid().ToString();

        public List<BeginNode> BeginNodes = new List<BeginNode>();

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

        /// <summary>
        ///     Refresh the index information for the current nodes
        /// </summary>
        /// <param name="basicNodeList">Current node list</param>
        public void Refresh(List<BasicNode> basicNodeList)
        {
            BeginNodes.Clear();
            AutoAddNodes(basicNodeList);
        }

        private bool NodeListEquals(NodeIndex other)
        {
            return other.BeginNodes.AreSimilar(BeginNodes);
        }

        /// <summary>
        ///     Returns the serialized field
        /// </summary>
        /// <returns>Json string</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object obj)
        {
            return obj is NodeIndex index && _id == index._id && NodeListEquals(index);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}
