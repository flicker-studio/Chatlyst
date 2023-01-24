using Newtonsoft.Json;
using NexusVisual.Editor;
using NexusVisual.Runtime;
using NUnit.Framework;
using UnityEngine;

public class Serialization
{
    [Test]
    public void File()
    {
        var b = FileUtilities.PathValidCheck("Samples/data.nvp");
        string a = "C:\\Users\\morsiusiurandum\\GitHub\\Nexus Visual\\Assets\\Samples\\data.nvp";
        Assert.AreEqual(b, a);
    }

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
}