using System;
using System.Collections.Generic;
using Chatlyst.Runtime.Data;
using Chatlyst.Runtime.Util;
using Newtonsoft.Json;

namespace Chatlyst.Runtime.Serialization
{
    /// <summary>
    ///     The node data directory, which is serialized to JSON and written directly to a file
    /// </summary>
    public class NodeDataIndex
    {
        [JsonProperty]
        private readonly string _id = Guid.NewGuid().ToString();

        #region Node Data List
        /// <summary>
        ///     The list of Begin Nodes
        /// </summary>
        public List<BeginNode> BeginNodesList = new List<BeginNode>();

        /// <summary>
        ///     The list of End Nodes
        /// </summary>
        public List<EndNode> EndNodesList = new List<EndNode>();
        #endregion

        #region API
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
                    BeginNodesList.Add((BeginNode)node);
                    break;
                case NodeType.BRA:
                    break;
                case NodeType.END:
                    EndNodesList.Add((EndNode)node);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Add a node to nodes list
        /// </summary>
        /// <inheritdoc cref="AutoAddNode" />
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
            BeginNodesList.Clear();
            AutoAddNodes(basicNodeList);
        }
        #endregion

        #region Override
        private bool NodeListEquals(NodeDataIndex other)
        {
            return other.BeginNodesList.AreSimilar(BeginNodesList);
        }

        /// <summary>
        ///     Returns the serialized field
        /// </summary>
        /// <returns>Json string</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        ///<inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is NodeDataIndex index && _id == index._id && NodeListEquals(index);
        }

        ///<inheritdoc />
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        #endregion
    }
}
