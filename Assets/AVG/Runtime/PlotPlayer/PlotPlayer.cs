using System.Collections.Generic;
using AVG.Runtime.PlotTree;
using Cysharp.Threading.Tasks;

namespace AVG.Runtime.PlotPlayer
{
    public class PlotPlayer : IPlotPlayer
    {
        public IPlotTree PlotTree;
        public Dictionary<string, Section> sectionsList { get; set; }
        public Section currentSection { get; set; }
        public Section GetSection(string guid) => sectionsList[guid];


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