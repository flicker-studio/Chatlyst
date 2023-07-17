using Chatlyst.Editor.Serialization;
using Chatlyst.Editor.Views;

namespace Chatlyst.Editor
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