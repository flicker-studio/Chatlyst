using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public class PlotEditor : EditorWindow
    {
        private PlotSo m_PlotSo;
        private bool m_HasStartNode;
        private PlotGraphView m_GraphView;
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

            if (m_PlotSo.dialogueSections == null || m_PlotSo.startSection == null)
            {
                m_PlotSo.ResetPlot();
                return;
            }

            #region Re-draw the plot tree

            m_PlotSo.DialogueSectionDictionary.Clear();
            for (var index = 0; index < m_PlotSo.seLength; index++)
            {
                var section = m_PlotSo.dialogueSections[index];

                m_PlotSo.DialogueSectionDictionary.Add(section.guid, index);
            }

            var temp = new Edge();

            var startNode = StartNode.NodeRedraw(m_GraphView, m_PlotSo.startSection, new StartNode());
            var next = m_PlotSo.startSection.next;
            temp.output = startNode.outputContainer[0].Q<Port>();

            for (;;)
            {
                if (string.IsNullOrEmpty(next)) break;

                var targetDialogue = m_PlotSo.dialogueSections[m_PlotSo.DialogueSectionDictionary[next]];
                var nextNode = (DialogueNode)DialogueNode.NodeRedraw(m_GraphView, targetDialogue, new DialogueNode());

                temp.input = nextNode.inputContainer[0].Q<Port>();

                temp.input.Connect(temp);
                temp.output.Connect(temp);
                m_GraphView.Add(temp);

                next = nextNode.Section.next;
                temp = new Edge
                {
                    input = nextNode.inputContainer[0].Q<Port>(),
                    output = nextNode.outputContainer[0].Q<Port>()
                };
            }

            #endregion
        }

        private void MenuTrigger(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            var currentMousePosition = Event.current.mousePosition;
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Node"), false,
                () => { DialogueNode.NodeAdd(m_GraphView, currentMousePosition, new DialogueNode()); });
            menu.AddItem(new GUIContent("Add Start"), false,
                () => { StartNode.NodeAdd(m_GraphView, currentMousePosition, new StartNode()); });
            menu.AddItem(new GUIContent("Save"), false,
                DataSave);
            menu.ShowAsContext();
        }

        private void DataSave()
        {
            m_HasStartNode = false;
            EditorUtility.SetDirty(m_PlotSo);
            m_PlotSo.ResetPlot();
            var edgeList = m_GraphView.edges.ToList();
            var nodeList = m_GraphView.nodes.ToList();

            #region Node Save

            foreach (var sectionNode in nodeList)
            {
                switch (sectionNode)
                {
                    case DialogueNode dialogueNode:
                        dialogueNode.Section.pos = dialogueNode.GetPosition();
                        m_PlotSo.dialogueSections.Add(dialogueNode.Section);
                        break;
                    case StartNode startNode:
                        if (m_HasStartNode)
                        {
                            Debug.Log("One more Start Node");
                        }
                        else
                        {
                            m_PlotSo.startSection = startNode.Section;
                            m_PlotSo.startSection.pos = startNode.GetPosition();
                            m_HasStartNode = true;
                        }

                        break;
                    default:
                        Debug.Log("Unknown Node");
                        break;
                }
            }

            for (var index = 0; index < m_PlotSo.seLength; index++)
            {
                var section = m_PlotSo.dialogueSections[index];
                m_PlotSo.DialogueSectionDictionary.Add(section.guid, index);
            }

            #endregion

            #region Link Sace

            foreach (var edge in edgeList)
            {
                var outputNode = edge.output.node;

                switch (outputNode)
                {
                    case DialogueNode current:
                        var thisGuid = current.Section.guid;
                        var nextNode = edge.input.node;
                        if (nextNode is DialogueNode dialogueNode)
                        {
                            var nextGuid = dialogueNode.Section.guid;
                            var index = m_PlotSo.DialogueSectionDictionary[thisGuid];
                            m_PlotSo.dialogueSections[index].next = nextGuid;
                        }
                        else
                        {
                            Debug.Log("DialogueNode");
                        }

                        break;
                    case StartNode:
                        var nextNodes = edge.input.node;
                        if (nextNodes is StartNode dialogueNodes)
                        {
                            Debug.Log("StartNode");
                            var nextGuid = dialogueNodes.Section.guid;
                            m_PlotSo.startSection.next = nextGuid;
                        }

                        break;
                    default:
                        Debug.Log("Unknown Node");
                        break;
                }
            }

            #endregion

            m_Title = "Plot Editor";
        }

        private void OnInspectorUpdate()
        {
            if (titleContent.text != m_Title) titleContent.text = m_Title;
        }
    }
}