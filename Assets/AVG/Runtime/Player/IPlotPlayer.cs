using System.Collections.Generic;
using AVG.Runtime.Controller;

namespace AVG.Runtime
{
    public interface IPlotPlayer : IBasicService
    {
        public Dictionary<string, ISection> sectionsList { get; }
        public ISection currentSection { get; set; }
        public ISection GetSection(string guid);
    }
}