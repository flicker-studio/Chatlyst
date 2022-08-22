namespace AVG.Runtime.PlotTree
{
    public interface IPlotTree
    {
        public ISection startSection { get; set; }
        public ISection GetNextSection(string guid);
        public ISection GetSection(string guid);
    }
}