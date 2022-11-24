using System.Collections.Generic;
using NexusVisual.Runtime.Controller;

namespace NexusVisual.Runtime
{
    public interface IPlotPlayer : IBasicService
    {
        public Dictionary<string, ISection> sectionsList { get; }
        public ISection currentSection { get; set; }
        public ISection GetSection(string guid);
    }
}