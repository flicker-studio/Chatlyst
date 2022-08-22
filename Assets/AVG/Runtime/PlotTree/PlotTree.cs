using System.Collections.Generic;

namespace AVG.Runtime.PlotTree
{
    public class PlotTree : IPlotTree
    {
        private Dictionary<string, ISection> plot { get; set; }
        public ISection startSection { get; set; }

        public ISection GetNextSection(string guid) => plot[GetSection(guid).nextGuid];


        public ISection GetSection(string guid) => plot[guid];
    }
}