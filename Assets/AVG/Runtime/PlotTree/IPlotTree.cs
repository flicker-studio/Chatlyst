namespace AVG.Runtime.PlotTree
{
    public interface IPlotTree
    {
        public Section startSection { get; }
        public Section GetNextSection(string guid);
        public Section GetSection(string guid);
    }
}