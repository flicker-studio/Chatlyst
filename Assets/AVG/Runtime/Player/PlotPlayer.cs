using System.Collections.Generic;
using AVG.Runtime.Controller;
using Cysharp.Threading.Tasks;
using UnityEditor;

//TODO:Editor only references
namespace AVG.Runtime
{
    public class PlotPlayer : IBasicService
    {
        public Dictionary<string, BaseSection> sections;
        public BaseSection StartSection { get; set; }
        public BaseSection currentSection;
        public BaseSection GetNextSection => sections[currentSection.Next];
        public BaseSection GetSection(string guid) => sections[guid];

        public UniTask InitializeAsync()
        {
            var plotSo = AssetDatabase.LoadAssetAtPath<PlotSo>("Assets/Editor Default Resources/TestPlot.asset");
            sections = plotSo.BaseSectionDic;
            StartSection = plotSo.startSections[0];
            currentSection = GetSection(StartSection.Next);
            return UniTask.CompletedTask;
        }


        public void UpdateThis()
        {
            currentSection = GetNextSection;
        }

        public void info(out string name, out string text)
        {
            name = ((DialogueSection)currentSection).characterName;
            text = ((DialogueSection)currentSection).dialogueText;
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}