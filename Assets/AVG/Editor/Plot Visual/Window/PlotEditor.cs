using System.Linq;
using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public class PlotEditor : EditorWindow
    {
        private PlotGraphView m_GraphView;
        private PlotSo m_PlotSo;
        private string m_Title = "Plot Editor";
        private const KeyCode MenuKey = KeyCode.Space;

        public static void DataEdit(PlotSo targetPlot)
        {
            var window = GetWindow<PlotEditor>("Plot Editor");
            window.m_PlotSo = targetPlot;
            window.GraphViewInitialize();
        }

        private void GraphViewInitialize()
        {
            m_GraphView ??= new PlotGraphView();
            m_GraphView.RegisterCallback<KeyDownEvent>(MenuTrigger);
            m_GraphView.StretchToParentSize();
            rootVisualElement.Add(m_GraphView);
            m_GraphView.graphViewChanged += (_ =>
            {
                m_Title = "Plot Editor(Unsaved)";
                return default;
            });

            if (m_PlotSo.dialogueSections == null)
            {
                m_PlotSo.ResetPlot();
                return;
            }

            #region Redraw the plot tree

            foreach (var dialogue in m_PlotSo.dialogueSections.ToList().Where(dialogue => dialogue != null))
            {
                m_GraphView.RedrawNode(dialogue);
            }

            var listDictionary = m_PlotSo.links.ToDictionary(link => link.guid);
            var nodeList = m_GraphView.nodes.ToList().Cast<DialogueNode>().ToList();
            var nodeDictionary = nodeList.ToDictionary(node => node.Section.guid);


            foreach (var temp in from node in m_GraphView.nodes.ToList().Cast<DialogueNode>().ToList()
                     where listDictionary.ContainsKey(node.Section.guid)
                     let link = listDictionary[node.Section.guid]
                     let targetNode = nodeDictionary[link.nextGuid]
                     select new Edge
                     {
                         output = node.outputContainer[0].Q<Port>(),
                         input = targetNode.inputContainer[0].Q<Port>(),
                     })
            {
                temp.input.Connect(temp);
                temp.output.Connect(temp);
                m_GraphView.Add(temp);
            }

            #endregion
        }

        private void MenuTrigger(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
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
            var edgeList = m_GraphView.edges.ToList();

            foreach (var sectionNode in m_GraphView.nodes.ToList().Cast<DialogueNode>())
            {
                sectionNode.Section.nodePos = sectionNode.GetPosition();
                m_PlotSo.dialogueSections.Add(sectionNode.Section);
            }

            //TODO:remove link data
            foreach (var edge in edgeList)
            {
                var output = edge.output.node as DialogueNode;
                var input = edge.input.node as DialogueNode;


                m_PlotSo.links.Add(new NodeLink()
                {
                    guid = output?.Section.guid,
                    nextGuid = input?.Section.guid,
                });
            }

            m_Title = "Plot Editor";
        }

        private void OnInspectorUpdate()
        {
            if (titleContent.text != m_Title) titleContent.text = m_Title;
        }
    }
}