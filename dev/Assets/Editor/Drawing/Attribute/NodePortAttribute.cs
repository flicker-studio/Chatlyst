using System;

namespace Chatlyst.Editor
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NodePortAttribute : Attribute
    {
        public readonly int InputPortNum;
        public readonly int OutputPortNum;

        public NodePortAttribute(int inNum, int outNum)
        {
            InputPortNum = inNum;
            OutputPortNum = outNum;
        }
    }
}