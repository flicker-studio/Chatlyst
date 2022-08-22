using System.Collections.Generic;
using AVG.Runtime.Controller;
using AVG.Runtime.PlotTree;

namespace AVG.Runtime.PlotPlayer
{
    public interface IPlotPlayer : IBasicService
    {
        Dictionary<string, ISection> sectionsList { get; set; }
        ISection currentSection { get; set; }
        public ISection GetSection(string guid);
    }
}