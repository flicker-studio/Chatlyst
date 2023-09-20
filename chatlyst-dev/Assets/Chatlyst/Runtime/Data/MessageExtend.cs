using Newtonsoft.Json;
namespace Chatlyst.Runtime
{
    public static class MessageExtend
    {
        public static string Serialize(this Message message)
        {
            return JsonConvert.SerializeObject(message);
        }

        public static Message DeserializeToMessage(this string message)
        {
            return JsonConvert.DeserializeObject<Message>(message);
        }
    }
}
