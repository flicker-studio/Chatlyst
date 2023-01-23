using System;
using Newtonsoft.Json;

namespace NexusVisual.Editor
{
    public static class ObjectExtensions
    {
        public static NexusJsonEntry ConvertToEntry(this object target)
        {
            var setting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var jsonString = JsonConvert.SerializeObject(target, setting);
            var id = Guid.NewGuid().ToString();
            return new NexusJsonEntry(target.GetType(), id, jsonString);
        }
    }
}