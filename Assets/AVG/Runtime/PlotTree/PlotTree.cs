using System.Collections.Generic;

namespace AVG.Runtime.PlotTree
{
    public class PlotTree : IPlotTree
    {
        public Dictionary<string, ISection> plot { get; set; }
        public ISection startSection { get; }

        public PlotTree(PlotSo so)
        {
            plot = so.sectionCollection.ToDictionary();
        }

        public ISection GetNextSection(string guid) => plot[GetSection(guid).Next];
        public ISection GetSection(string guid) => plot[guid];
    }
}