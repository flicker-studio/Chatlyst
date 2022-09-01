using System.Collections.Generic;

namespace AVG.Runtime.PlotTree
{
    public class PlotTree : IPlotTree
    {
        public Dictionary<string, ISection> plot { get; set; }
        public ISection startSection { get; }

        public PlotTree(PlotSo so)
        {
            startSection = so.startSection;
            plot = so.ToDictionary();
        }

        public ISection GetNextSection(string guid) => plot[GetSection(guid).next];
        public ISection GetSection(string guid) => plot[guid];
    }
}