using System.Collections.Generic;
using AVG.Runtime.Controller;
using AVG.Runtime.PlotTree;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

//TODO:Editor only references
namespace AVG.Runtime.PlotPlayer
{
    public class PlotPlayer : IBasicService
    {
        public Dictionary<string, ISection> sections;
        public ISection StartSection { get; set; }
        public ISection currentSection;
        public ISection GetNextSection => sections[currentSection.Next];
        public ISection GetSection(string guid) => sections[guid];

        public UniTask InitializeAsync()
        {
            var plotSo = AssetDatabase.LoadAssetAtPath<PlotSo>("Assets/Editor Default Resources/TestPlot.asset");
            sections = plotSo.sectionCollection.ToDictionary();
            StartSection = plotSo.sectionCollection.startSections[0];
            currentSection = GetSection(StartSection.Next);
            EngineCore.Player = this;
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