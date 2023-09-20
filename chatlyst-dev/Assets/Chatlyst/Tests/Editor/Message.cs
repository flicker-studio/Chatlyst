using Chatlyst.Runtime;
using NUnit.Framework;
using Tests.Utility;

namespace Tests.Editor
{
    /// <summary>
    ///     Unit tests for message
    /// </summary>
    public class Message
    {
        [Test]
        public void Serialize()
        {
            var message        = new Chatlyst.Runtime.Message(Utilities.RandomString(5), Utilities.RandomString(8), Utilities.RandomString(2));
            var afterSerialize = message.Serialize().DeserializeToMessage();
            Assert.AreEqual(message, afterSerialize);
        }
    }
}
