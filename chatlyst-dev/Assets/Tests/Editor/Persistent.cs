using System.Collections.Generic;
using Chatlyst.Runtime;
using NUnit.Framework;
using UnityEngine;

public class NodeIndexTest
{
    [Test]
    public void AutoAddNodeFunction()
    {
        var begins =
            new List<BasicNode>
            {
                new BeginNode("Start Label", 2),
                new BeginNode("Start Label", 3)
            };
        var index = new NodeIndex();
        index.AutoAddNodes(begins);
        string json = index.ToJson();
        Debug.Log(json);
        var deserialize = NodeIndex.DeserializeFromJson(json);
        Assert.AreEqual(index, deserialize);
    }
}
