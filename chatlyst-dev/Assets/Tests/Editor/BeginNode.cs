using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using Tests.Utility;

namespace Tests.Editor
{
    /// <summary>
    ///     Unit tests for <see cref="BeginNode" />
    /// </summary>
    public class BeginNode
    {
        [Test]
        public void ClassDeserialize()
        {
            var    begin = DataNode.GetBeginNode();
            string ser   = begin.ToString();
            var    ans   = JsonConvert.DeserializeObject<Chatlyst.Runtime.BeginNode>(ser);
            Assert.AreEqual(begin, ans);
        }

        [Test]
        public void ListDeserialize()
        {
            var    list1 = DataNode.GetBeginNodeList(5);
            string str   = JsonConvert.SerializeObject(list1);
            var    list2 = JsonConvert.DeserializeObject<List<Chatlyst.Runtime.BeginNode>>(str);
            Assert.AreEqual(list1, list2);
        }
    }
}
