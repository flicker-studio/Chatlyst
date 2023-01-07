namespace NexusVisual.Runtime
{
    /*
        public class PlotPlayer : IBasicService
        {
            public Dictionary<string, BaseData> sections;
            public BaseData startData { get; set; }
            public BaseData CurrentData;
            public BaseData getNextData => sections[CurrentData.Next];
            public BaseData GetSection(string guid) => sections[guid];
    
            public UniTask InitializeAsync()
            {
                var plotSo = AssetDatabase.LoadAssetAtPath<PlotSo>("Assets/Editor Default Resources/TestPlot.asset");
                sections = plotSo.BaseSectionDic;
                startData = plotSo.startSections[0];
                CurrentData = GetSection(startData.Next);
                return UniTask.CompletedTask;
            }
    
    
            public void UpdateThis()
            {
                CurrentData = getNextData;
            }
    
            public void info(out string name, out string text)
            {
                name = ((DialogueData)CurrentData).dialogueList .ToString();
                text = ((DialogueData)CurrentData).
            }
    
            public void Destroy()
            {
                throw new System.NotImplementedException();
            }
        }*/
}