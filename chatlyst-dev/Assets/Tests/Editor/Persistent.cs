using Chatlyst.Runtime;
using Newtonsoft.Json;
using NUnit.Framework;

public class NodeIndexTest
{
    [Test]
    public void Sample()
    {
        var begin = new BeginNode("Start Label", 2);
        NodeIndex index = new NodeIndex();
        index.BeginNodes.Add(begin);
        var json = index.ToJson();
        NodeIndex deserialize = JsonConvert.DeserializeObject<NodeIndex>(json);
        Assert.AreEqual(index, deserialize);
    }
}
