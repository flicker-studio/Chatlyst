using System.Linq;
using Newtonsoft.Json;
using NexusVisual.Editor;
using NexusVisual.Editor.Data;
using NexusVisual.Editor.Views;
using NexusVisual.Runtime;
using NUnit.Framework;
using UnityEngine;
using Dialogue = NexusVisual.Editor.Data.Dialogue;

public class Serialization
{
    [Test]
    public void SerializationFile()
    {
        var a = new NexusJsonEntry(typeof(int), "id", "json");
        var jsonString = JsonConvert.SerializeObject(a);
        NexusJsonEntry entry = JsonConvert.DeserializeObject<NexusJsonEntry>(jsonString);
        Assert.AreEqual(entry, a);
    }

    [Test]
    public void ConvertToEntry()
    {
        var b = ScriptableObject.CreateInstance<StartNvData>();
        var entry = b.ConvertToEntry();
        var o = entry.ConvertToOrigin<StartNvData>();
        Assert.AreEqual(b, o);
    }

    [Test]
    public void DialogueDataCovert()
    {
        var dias = new Dialogue("we:can");
        var data = new DialoguesNode();
        data.DialogueList.Add(dias);
        var entry = data.ConvertToEntry();
        var restore = entry.ConvertToOrigin<DialoguesNode>();
        Assert.AreEqual(dias, restore.DialogueList.First());
        Assert.AreEqual(data.Guid, restore.Guid);
    }

    [Test]
    public void NodeCovert()
    {
        var startNode = new StartNode("StartTest");
        var jsonEntry = startNode.ConvertToEntry();
        var startView = new StartNodeView(jsonEntry);
        var newNode = startView.dataEntry.ConvertToOrigin<StartNode>();
        Assert.AreEqual(startNode, newNode);
    }
}