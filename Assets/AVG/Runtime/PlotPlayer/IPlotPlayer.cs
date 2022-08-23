using System.Collections.Generic;
using AVG.Runtime.Controller;
using AVG.Runtime.PlotTree;

namespace AVG.Runtime.PlotPlayer
{
    public interface IPlotPlayer : IBasicService
    {
        Dictionary<string, Section> sectionsList { get; set; }
        Section currentSection { get; set; }
        public Section GetSection(string guid);
    }
}