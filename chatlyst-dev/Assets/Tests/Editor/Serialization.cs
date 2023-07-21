using System;
using System.Linq;
using Chatlyst.Runtime;
using NUnit.Framework;

public class Serialization
{
    private static readonly Random Random = new Random();
    private static string RandomString(int length)
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(characters, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    [Test]
    public void MessageSerialize()
    {
        var message = new Message(RandomString(5), RandomString(8), RandomString(2));
        var afterSerialize = message.Serialize().DeserializeToMessage();
        Assert.AreEqual(message, afterSerialize);
    }
}
