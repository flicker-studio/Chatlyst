using System.Collections.Generic;
using Chatlyst.Runtime;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
namespace Tests.Editor.Data
{

    /// <summary>
    ///     Unit tests for begin-node
    /// </summary>
    public class BeginNodeTest
    {
        [Test]
        public void ListSet()
        {
            var begin = new BeginNode("Start Label", 2);
            var list1 = new List<BeginNode>();
            string str = JsonConvert.SerializeObject(list1);
            var list2 = JsonConvert.DeserializeObject<List<BeginNode>>(str);
            Assert.AreEqual(list1, list2);
        }

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
}
