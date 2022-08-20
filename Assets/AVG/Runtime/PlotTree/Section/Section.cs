namespace AVG.Runtime.PlotTree
{
    public class Section
    {
        public string Guid;

        protected Section(bool dataNull)
        {
            if (dataNull)
            {
                Guid = System.Guid.NewGuid().ToString();
            }
        }
    }
}