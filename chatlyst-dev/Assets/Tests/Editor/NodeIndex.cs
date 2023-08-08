using System.Collections.Generic;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor
{
    public class NodeIndex
    {
        [Test]
        public void AutoAddNodeFunction()
        {
            var begins =
                new List<BasicNode>
                {
                    new Chatlyst.Runtime.BeginNode("Start Label", 2),
                    new Chatlyst.Runtime.BeginNode("Start Label", 3)
                };
            var index = new Chatlyst.Runtime.NodeIndex();
            index.AutoAddNodes(begins);
            string json = index.ToString();
            Debug.Log(json);
            var deserialize = IndexJsonInternal.Deserialize(json);
            Assert.AreEqual(index, deserialize);
        }
    }
}
