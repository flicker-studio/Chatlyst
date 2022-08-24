using System.Collections.Generic;
using AVG.Runtime.Controller;
using AVG.Runtime.PlotTree;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

//TODO:Editor only references
namespace AVG.Runtime.PlotPlayer
{
    public class PlotPlayer : IPlotPlayer
    {
        public IPlotTree PlotTree;
        public Dictionary<string, ISection> sectionsList => PlotTree.plot;
        public ISection currentSection { get; set; }
        public ISection GetSection(string guid) => sectionsList[guid];


        public UniTask InitializeAsync()
        {
            var plotSo = EditorGUIUtility.Load("TestPlot.asset") as PlotSo;
            PlotTree = new PlotTree.PlotTree(plotSo);
            EngineCore.runtimeBehavior.OnMonoStart += Show;
            return UniTask.CompletedTask;
        }

        public void Show()
        {
            DialogueSection value;

            foreach (var section in sectionsList)
            {
                value = section.Value as DialogueSection;
                Debug.Log(value?.characterName + ":" + value?.dialogueText);
            }
        }

        public void Destroy()
        {
            throw new System.NotImplementedException();
        }
    }
}