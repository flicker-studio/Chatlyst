using System.Collections.Generic;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime.Data;
using Chatlyst.Runtime.Serialization;
using NUnit.Framework;
using Tests.Utility;

namespace Tests.Editor
{
    public class NodeIndex
    {
        [Test]
        public void AutoAddNodeMethod()
        {
            var beginNodeList = DataNode.GetBeginNodeList(3);
            var basicNodeList = new List<BasicNode>();
            var nodeIndex     = new NodeDataIndex();
            basicNodeList.AddRange(beginNodeList);
            nodeIndex.AutoAddNodes(basicNodeList);
            string json                  = nodeIndex.ToString();
            var    deserializedNodeIndex = IndexJsonInternal.Deserialize(json);
            Assert.AreEqual(nodeIndex, deserializedNodeIndex);
        }
    }
}
