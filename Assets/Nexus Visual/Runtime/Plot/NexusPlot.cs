using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
namespace NexusVisual.Runtime
{
    [Serializable]
    public class NexusPlot
    {
        public Dictionary<string, BaseNvData> NodeData;

        private string _filePath;


        public static void CreatFile()
        {
        }

        public NexusPlot()
        {
            _filePath = null;
            NodeData = new Dictionary<string, BaseNvData>();
        }

        public NexusPlot(string filePath)
        {
            var jsonString = File.ReadAllText(filePath);
            var copy = JsonConvert.DeserializeObject<NexusPlot>(jsonString);
            //Data copy
            _filePath = filePath;
            NodeData = copy.NodeData;
        }

        public void SavePlot()
        {
            var setting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var jsonString = JsonConvert.SerializeObject(this, typeof(NexusPlot), setting);
            File.WriteAllText(_filePath, jsonString);
        }
    }
}
