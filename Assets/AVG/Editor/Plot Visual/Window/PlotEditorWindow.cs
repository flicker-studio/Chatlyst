using System.Linq;
using AVG.Runtime.Plot;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//TODO:Editor Open Show
namespace AVG.Editor.Plot_Visual
{
    public class PlotEditorWindow : EditorWindow
    {
        private PlotGraphView m_GraphView;
        private PlotSo m_PlotSo;

        public static void Edit(PlotSo targetPlot)
        {
            var window = GetWindow<PlotEditorWindow>("Plot Editor");
            window.m_PlotSo = targetPlot;
            window.GraphViewInitialize();
        }

        private void GraphViewInitialize()
        {
            m_GraphView = new PlotGraphView();
            m_GraphView.RegisterCallback<KeyDownEvent>(SpaceKeyMenu);
            m_GraphView.StretchToParentSize();
            rootVisualElement.Add(m_GraphView);
        }

        private void SpaceKeyMenu(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != KeyCode.Space) return;
            var currentMousePosition = Event.current.mousePosition;
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Node"), false,
                () => { m_GraphView.AddNode(currentMousePosition); });
            menu.AddItem(new GUIContent("Save"), false,
                DataSave);
            menu.ShowAsContext();
        }


        private void DataSave()
        {
            EditorUtility.SetDirty(m_PlotSo);
            m_PlotSo.ResetPlot();

            foreach (var sectionNode in m_GraphView.nodes.ToList().Cast<SectionNode>())
            {
                m_PlotSo.nodes.Add(
                    new SectionData(sectionNode.SectionData));
            }

            var edges = m_GraphView.edges.ToList();
            for (var i = 0; i < edges.Count; i++)
            {
                var output = edges[i].output.node as SectionNode;
                var input = edges[i].input.node as SectionNode;

                m_PlotSo.links.Add(new NodeLink()
                {
                    guid = output?.SectionData.guid,
                    nextGuid = input?.SectionData.guid,
                    portId = i
                });
            }
        }
    }
}