using Chatlyst.Runtime;
using NUnit.Framework;
namespace Tests.Editor.Data
{
    /// <summary>
    ///Unit tests for message
    /// </summary>
    public class MessageTest
    {
        [Test]
        public void Serialize()
        {
            var message = new Message(Utilities.RandomString(5), Utilities.RandomString(8), Utilities.RandomString(2));
            var afterSerialize = message.Serialize().DeserializeToMessage();
            Assert.AreEqual(message, afterSerialize);
        }
    }
}

