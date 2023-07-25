using Chatlyst.Runtime;
using Newtonsoft.Json;
using NUnit.Framework;

public class NodeDataTest
{
    [Test]
    public void BaseNodeSerialization()
    {
        var begin = new BeginNode("Start Label");
        BaseNode baseNode = begin.ToBaseNode();
        var s = JsonConvert.SerializeObject(baseNode);
        var cov = JsonConvert.DeserializeObject<BaseNode>(s);
        Assert.AreEqual(baseNode, cov);
    }

    [Test]
    public void BeginNodeSerialization()
    {
        var begin = new BeginNode("Start Label");
        var ser = begin.ToJson();
        var ans = JsonConvert.DeserializeObject<BeginNode>(ser);
        Assert.AreEqual(begin.ToString(), ans.ToString());
    }

    [Test]
    public void BeginNodeConversionToBase()
    {
        var begin = new BeginNode("Start Label");
        BaseNode bn = begin.ToBaseNode();
        bn.TryToSource(out BeginNode ans);
        Assert.AreEqual(begin, ans);
    }
}
