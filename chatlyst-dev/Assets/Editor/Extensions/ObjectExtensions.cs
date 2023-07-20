using System;
using Chatlyst.Editor.Serialization;
using Newtonsoft.Json;

namespace Chatlyst.Editor
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