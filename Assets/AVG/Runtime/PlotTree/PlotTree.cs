using System.Collections.Generic;
using System.Linq;

namespace AVG.Runtime.PlotTree
{
    public class PlotTree : IPlotTree
    {
        private Dictionary<string, Section> m_Plot;
        public Section startSection { get; }

        //Load ScriptableObject to Dictionary
        public PlotTree(PlotSo so)
        {
            m_Plot = so.nodes.ToDictionary(data => data.guid);
            startSection = GetSection(so.startGuid);
        }

        public Section GetNextSection(string guid) => m_Plot[GetSection(guid).nextGuid];
        public Section GetSection(string guid) => m_Plot[guid];
    }
}