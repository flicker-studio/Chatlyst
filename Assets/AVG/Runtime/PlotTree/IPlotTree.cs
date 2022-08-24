using System.Collections.Generic;

namespace AVG.Runtime.PlotTree
{
    public interface IPlotTree
    {
        public Dictionary<string, ISection> plot { get; set; }
        public ISection startSection { get; }
        public ISection GetNextSection(string guid);
        public ISection GetSection(string guid);
    }
}