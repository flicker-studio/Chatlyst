using System.Collections.Generic;
using AVG.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor
{
    internal class PlotSoEditorWindow : EditorWindow
    {
        private PlotSo _plotSo;
        private PlotSoGraphView _soGraphView;
        private string _title = "Plot Editor";
        private const KeyCode MenuKey = KeyCode.Space;


        public static void DataEdit(PlotSo targetPlot)
        {
            var window = GetWindow<PlotSoEditorWindow>("Plot Editor");
            window._plotSo = targetPlot;
            window.GraphViewInitialize();
        }

        private void GraphViewInitialize()
        {
            _soGraphView ??= new PlotSoGraphView();
            _soGraphView.RegisterCallback<KeyDownEvent>(MenuTrigger);
            _soGraphView.StretchToParentSize();
            rootVisualElement.Add(_soGraphView);
            _soGraphView.graphViewChanged += (_ =>
            {
                _title = "Plot Editor(Unsaved)";
                return default;
            });

            #region Re-draw the plot tree

            var sectionDictionary = _plotSo.BaseSectionDic;
            var nodeDictionary = new Dictionary<string, Node>();

            foreach (var section in sectionDictionary.Values)
            {
                Node node;
                switch (section)
                {
                    case StartSection startSection:
                        node = (StartNode)StartNode.NodeRedraw(_soGraphView, new StartNode(startSection));
                        nodeDictionary.Add(section.Guid, node);
                        break;
                    case DialogueSection dialogueSection:
                        node = (DialogueNode)DialogueNode.NodeRedraw(_soGraphView, new DialogueNode(dialogueSection));
                        nodeDictionary.Add(section.Guid, node);
                        break;
                    default:
                        Debug.Log("Unknown BaseSection");
                        break;
                }
            }

            foreach (var section in sectionDictionary.Values)
            {
                if (!string.IsNullOrEmpty(section.Next))
                {
                    var edge = new Edge
                    {
                        output = nodeDictionary[section.Guid].outputContainer[0].Q<Port>(),
                        input = nodeDictionary[section.Next].inputContainer[0].Q<Port>()
                    };
                    edge.input.Connect(edge);
                    edge.output.Connect(edge);
                    _soGraphView.AddElement(edge);
                }
            }

            #endregion
        }

        private void MenuTrigger(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            var currentMousePosition = Event.current.mousePosition;
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Node"), false,
                () => { DialogueNode.NodeAdd(_soGraphView, currentMousePosition, new DialogueNode()); });
            menu.AddItem(new GUIContent("Add Start"), false,
                () => { StartNode.NodeAdd(_soGraphView, currentMousePosition, new StartNode()); });
            menu.AddItem(new GUIContent("Save"), false,
                DataSave);
            menu.ShowAsContext();
        }

        private void DataSave()
        {
            EditorUtility.SetDirty(_plotSo);

            var collection = new SectionCollection();

            var edgeList = _soGraphView.edges.ToList();
            var nodeList = _soGraphView.nodes.ToList();

            #region Node Save

            foreach (var sectionNode in nodeList)
            {
                switch (sectionNode)
                {
                    case DialogueNode dialogueNode:
                        dialogueNode.Section.Pos = dialogueNode.GetPosition();
                        collection.dialogueSections.Add(dialogueNode.Section);
                        break;
                    case StartNode startNode:
                        startNode.Section.Pos = startNode.GetPosition();
                        collection.startSections.Add(startNode.Section);
                        break;
                    default:
                        Debug.Log("Unknown Node");
                        break;
                }
            }

            #endregion

            #region Link Save

            var sectionDictionary = collection.ToDictionary();
            foreach (var edge in edgeList)
            {
                var outputNode = edge.output.node;
                var inputNode = edge.input.node;
                if (outputNode is IGraphNode current && inputNode is IGraphNode next)
                {
                    var thisGuid = current.Guid;
                    var nextGuid = next.Guid;
                    sectionDictionary[thisGuid].Next = nextGuid;
                }
            }

            _plotSo.SectionCollect(sectionDictionary);

            #endregion

            _title = "Plot Editor";
        }

        private void OnInspectorUpdate()
        {
            if (titleContent.text != _title) titleContent.text = _title;
        }
    }
}