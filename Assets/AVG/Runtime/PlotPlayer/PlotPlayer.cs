using System.Collections.Generic;
using AVG.Runtime.PlotTree;
using Cysharp.Threading.Tasks;

namespace AVG.Runtime.PlotPlayer
{
    public class PlotPlayer : IPlotPlayer
    {
        public IPlotTree PlotTree;
        public Dictionary<string, ISection> sectionsList { get; set; }
        public ISection currentSection { get; set; }
        public ISection GetSection(string guid) => sectionsList[guid];


        public UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}