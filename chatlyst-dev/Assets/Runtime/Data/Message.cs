namespace Chatlyst.Runtime
{
    public struct Message
    {
        public string name;
        public string talk;
        public string emotion;


        public Message(string name, string talk, string emotion)
        {
            this.name = name;
            this.talk = talk;
            this.emotion = emotion;
        }
    }
}
