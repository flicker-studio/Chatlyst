using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    internal class PlotEditor : EditorWindow
    {
        private PlotSo _plotSo;
        private bool _hasStartNode;
        private PlotGraphView _graphView;
        private string _title = "Plot Editor";
        private const KeyCode MenuKey = KeyCode.Space;


        public static void DataEdit(PlotSo targetPlot)
        {
            var window = GetWindow<PlotEditor>("Plot Editor");
            window._plotSo = targetPlot;
            window.GraphViewInitialize();
        }

        private void GraphViewInitialize()
        {
            _graphView ??= new PlotGraphView();
            _graphView.RegisterCallback<KeyDownEvent>(MenuTrigger);
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
            _graphView.graphViewChanged += (_ =>
            {
                _title = "Plot Editor(Unsaved)";
                return default;
            });

            if (_plotSo.dialogueSections == null || _plotSo.startSection == null)
            {
                _plotSo.Reset();
                return;
            }

            #region Re-draw the plot tree

            var sectionDictionary = _plotSo.ToDictionary();
            var temp = new Edge();
            var startNode = StartNode.NodeRedraw(_graphView, _plotSo.startSection, new StartNode());
            var next = _plotSo.startSection.next;
            temp.output = startNode.outputContainer[0].Q<Port>();

            for (;;)
            {
                if (string.IsNullOrEmpty(next)) break;

                var targetDialogue = sectionDictionary[next] as DialogueSection;
                var nextNode = (DialogueNode)DialogueNode.NodeRedraw(_graphView, targetDialogue, new DialogueNode());

                temp.input = nextNode.inputContainer[0].Q<Port>();

                temp.input.Connect(temp);
                temp.output.Connect(temp);
                _graphView.Add(temp);

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
                () => { DialogueNode.NodeAdd(_graphView, currentMousePosition, new DialogueNode()); });
            menu.AddItem(new GUIContent("Add Start"), false,
                () => { StartNode.NodeAdd(_graphView, currentMousePosition, new StartNode()); });
            menu.AddItem(new GUIContent("Save"), false,
                DataSave);
            menu.ShowAsContext();
        }

        private void DataSave()
        {
            _hasStartNode = false;
            EditorUtility.SetDirty(_plotSo);
            _plotSo.Reset();
            var edgeList = _graphView.edges.ToList();
            var nodeList = _graphView.nodes.ToList();

            #region Node Save

            foreach (var sectionNode in nodeList)
            {
                switch (sectionNode)
                {
                    case DialogueNode dialogueNode:
                        dialogueNode.Section.pos = dialogueNode.GetPosition();
                        _plotSo.dialogueSections.Add(dialogueNode.Section);
                        break;
                    case StartNode startNode:
                        if (_hasStartNode)
                        {
                            Debug.Log("One more Start Node");
                        }
                        else
                        {
                            startNode.Section.pos = startNode.GetPosition();
                            _plotSo.startSection = startNode.Section;
                            _hasStartNode = true;
                        }

                        break;
                    default:
                        Debug.Log("Unknown Node");
                        break;
                }
            }

            #endregion

            #region Link Save

            var sectionDictionary = _plotSo.ToDictionary();
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
                            var index = sectionDictionary[thisGuid];
                            //TODO:Add nextGuid
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
                            _plotSo.startSection.next = nextGuid;
                        }

                        break;
                    default:
                        Debug.Log("Unknown Node");
                        break;
                }
            }

            #endregion

            _title = "Plot Editor";
        }

        private void OnInspectorUpdate()
        {
            if (titleContent.text != _title) titleContent.text = _title;
        }
    }
}