using System;
using Newtonsoft.Json;
using NexusVisual.Editor.Serialization;

namespace NexusVisual.Editor
{
    public static class ObjectExtensions
    {
        public static NexusJsonEntity ConvertToEntity(this object target)
        {
            var jsonString = JsonConvert.SerializeObject(target, Formatting.Indented, NexusJsonUtility.IgnoreLoopSetting);
            var id = Guid.NewGuid().ToString();
            return new NexusJsonEntity();
        }
    }
}