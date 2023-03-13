using NexusVisual.Editor.Serialization;
using NexusVisual.Editor.Views;

namespace NexusVisual.Editor
{
    public static class ViewExtensions
    {
        public static NexusJsonEntity ViewToEntity(this NexusNodeView target)
        {
            var returns = target.ConvertToEntity();

            return returns;
        }
    }
}