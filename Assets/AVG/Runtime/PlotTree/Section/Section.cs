namespace AVG.Runtime.PlotTree
{
    public interface ISection
    {
        public string guid { get; set; }
        public string nextGuid { get; set; }
        //public ISection NextSection();
        //public ISection CurrentSection();
    }
}