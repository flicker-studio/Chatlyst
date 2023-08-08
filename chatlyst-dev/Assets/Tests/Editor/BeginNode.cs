using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor
{
    /// <summary>
    ///     Unit tests for begin-node
    /// </summary>
    public class BeginNode
    {
        [Test]
        public void ListSet()
        {
            var    begin = new Chatlyst.Runtime.BeginNode("Start Label", 2);
            var    list1 = new List<Chatlyst.Runtime.BeginNode>();
            string str   = JsonConvert.SerializeObject(list1);
            var    list2 = JsonConvert.DeserializeObject<List<Chatlyst.Runtime.BeginNode>>(str);
            Assert.AreEqual(list1, list2);
        }

        [Test]
        public void Serialization()
        {
            var    begin = new Chatlyst.Runtime.BeginNode("Start Label", 2);
            string ser   = begin.ToString();
            var    ans   = JsonConvert.DeserializeObject<Chatlyst.Runtime.BeginNode>(ser);
            Debug.Log(ser);
            Assert.AreEqual(begin, ans);
        }
    }
}
