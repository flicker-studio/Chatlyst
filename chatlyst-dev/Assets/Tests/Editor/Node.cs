using Chatlyst.Runtime;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;

/// <summary>
///Unit tests for message
/// </summary>
public class MessageTest
{
    [Test]
    public void MessageSerialize()
    {
        var message = new Message(Utilities.RandomString(5), Utilities.RandomString(8), Utilities.RandomString(2));
        var afterSerialize = message.Serialize().DeserializeToMessage();
        Assert.AreEqual(message, afterSerialize);
    }
}

/// <summary>
///Unit tests for base-node
/// </summary>
public class BaseNodeTest
{
    [Test]
    public void Serialization()
    {
        var begin = new BeginNode("Start Label", 2);
        var baseNode = begin.ToBaseNode();
        string s = JsonConvert.SerializeObject(baseNode);
        Debug.Log(s);
        var cov = JsonConvert.DeserializeObject<BaseNode>(s);
        Assert.AreEqual(baseNode, cov);
    }
}

/// <summary>
///Unit tests for begin-node
/// </summary>
public class BeginNodeTest
{
    [Test]
    public void Serialization()
    {
        var begin = new BeginNode("Start Label", 2);
        string ser = begin.ToJson();
        var ans = JsonConvert.DeserializeObject<BeginNode>(ser);
        Debug.Log(ser);
        Assert.AreEqual(begin, ans);
    }

    [Test]
    public void ConversionToBase()
    {
        var begin = new BeginNode("Start Label", 1);
        var bn = begin.ToBaseNode();
        bn.TryToSource(out BeginNode ans);
        Assert.AreEqual(begin, ans);
    }
}
