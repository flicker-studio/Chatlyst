using System;

namespace AVG.Runtime.PlotTree
{
    [Serializable]
    public class Section
    {
        public string guid { get; set; }
        public string nextGuid { get; set; }

        public Section()
        {
            guid = Guid.NewGuid().ToString();
            nextGuid = null;
        }
    }
}