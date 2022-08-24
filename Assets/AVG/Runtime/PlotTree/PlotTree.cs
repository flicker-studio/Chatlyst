using System.Collections.Generic;

namespace AVG.Runtime.PlotTree
{
    public class PlotTree : IPlotTree
    {
        public Dictionary<string, ISection> plot { get; set; }
        public ISection startSection { get; }

        public PlotTree(PlotSo so)
        {
            startSection = so.StartSection;
            plot = new Dictionary<string, ISection>();
            plot.Clear();
            foreach (var dialogue in so.dialogueSections)
            {
                plot.Add(dialogue.guid, dialogue);
            }
        }

        public ISection GetNextSection(string guid) => plot[GetSection(guid).nextGuid];
        public ISection GetSection(string guid) => plot[guid];
    }
}