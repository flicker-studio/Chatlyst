using System.Collections.Generic;
using Newtonsoft.Json;

namespace NexusVisual.Runtime
{
    public static class PlotSaver
    {
        public static string Save(PlotSo targetFile)
        {
            var setting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(targetFile, typeof(PlotSo), setting);
        }

        public static PlotSo Restore(string jsonData)
        {
            return JsonConvert.DeserializeObject<PlotSo>(jsonData);
        }
    }
}