using Newtonsoft.Json;
using NexusVisual.Editor;
using NUnit.Framework;

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
}