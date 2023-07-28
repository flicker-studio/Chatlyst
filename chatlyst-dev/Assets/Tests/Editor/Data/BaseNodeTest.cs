using Chatlyst.Runtime;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
namespace Tests.Editor.Data
{
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

}
