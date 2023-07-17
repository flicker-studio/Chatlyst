using System;
using System.Collections.Generic;
using System.Linq;
using Chatlyst.Editor.Data;
using Chatlyst.Editor.Serialization;
using NUnit.Framework;

public class Serialization
{
    [Test]
    public void A()
    {
        List<object> a = new List<object>();
        for (int i = 0; i < 3; i++)
        {
            a.Add(new Dialogue(Guid.NewGuid().ToString() + ":" + "hh"));
        }


        var stringText = NexusJsonUtility.SerializeIEnumerable(a);
        var abs = NexusJsonUtility.DeserializeIEnumerable<Dialogue>(stringText).ToList();
        //   Debug.Log(NexusJsonUtility.Tests(a));


        Assert.AreEqual(a, abs);
    }
}