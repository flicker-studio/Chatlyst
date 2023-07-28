using System;
using System.Collections.Generic;
using Chatlyst.Runtime.Util;
using Newtonsoft.Json;
using UnityEditor.Graphs;
namespace Chatlyst.Runtime
{
    public class NodeIndex
    {
        [JsonIgnore]
        private readonly Graph _graph = null;
        [JsonProperty]
        private readonly string _id;

        public List<BeginNode> BeginNodes = new List<BeginNode>();



        public NodeIndex()
        {
            _id = Guid.NewGuid().ToString();
            //_graph = graph;
        }

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

        public bool RefreshLists()
        {
            //if (_graph == null)
            {
                return false;
            }

            //Do something

            return true;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
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
