using NexusVisual.Runtime;
using UnityEngine;

namespace NexusVisual.Editor
{
    public sealed class Choice : BaseNvNode<ChoiceData>, IVisible
    {
        public Choice(ChoiceData nodeNvData = null, Rect targetPos = new Rect())
        {
            visualTree = CustomSettingProvider.GetSettings().nodeSetting.dialogueNode;
            Construction(nodeNvData, targetPos);
        }

        private protected override void DataBind()
        {
            return;
            throw new System.NotImplementedException();
        }
    }
}