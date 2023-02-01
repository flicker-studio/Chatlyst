using System;
using System.Collections.Generic;
using System.Linq;
using NexusVisual.Editor.Views;
using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    internal class PlotSoEditorWindow : EditorWindow
    {
        //Data and config
        private PlotSo _plotSo;
        private const KeyCode MenuKey = KeyCode.Space;

        //Basic element
        private PlotSoGraphView _graphView;
        private static PlotSoEditorWindow _window;

        //Toolbar element
        private ToolbarMenu _toolbarMenu;
        private ToolbarToggle _inspectorToggle;
        private ToolbarToggle _autoSaveToggle;
        private ToolbarButton _save;


        private void WindowInitialize()
        {
            #region Initialize editor window

            var visualTree = EditorGUIUtility.Load("NodeEditorWindow.uxml") as VisualTreeAsset;
            if (!visualTree) throw new Exception("Can not find EditorWindow.uxml");
            visualTree.CloneTree(rootVisualElement);
            saveChangesMessage = "未保存的更改!\n您是否要保存？";
            titleContent.text = $"{_plotSo.name}";

            #endregion

            #region Get visual element

            _graphView = rootVisualElement.Q<PlotSoGraphView>("GraphView");
            _toolbarMenu = rootVisualElement.Q<ToolbarMenu>("Menu");
            _inspectorToggle = rootVisualElement.Q<ToolbarToggle>("Inspector");
            _autoSaveToggle = rootVisualElement.Q<ToolbarToggle>("AutoSave");
            _save = rootVisualElement.Q<ToolbarButton>("Save");

            #endregion

            #region Action bind

            _graphView.RegisterCallback<KeyDownEvent>(SearchTreeBuild);
            _graphView.graphViewChanged += (_ =>
            {
                hasUnsavedChanges = true;
                return default;
            });
            _save.clicked += SaveChanges;
            ToolBarMenuAction();

            #endregion

            NodeRebuild();
        }

        //Todo:Use cache to rebuild faster
        private void NodeRebuild()
        {
            var dataDictionary = _plotSo.nodesData;
            if (dataDictionary == null) return;
            var nodeDictionary = new Dictionary<string, Node>();
            foreach (var section in dataDictionary.Values)
            {
                Node node;
                switch (section)
                {
                    case StartNvData startSection:
                        node = new StartNode(startSection);
                        _graphView.AddElement(node);
                        nodeDictionary.Add(section.guid, node);
                        break;
                    case DialogueNvData dialogueSection:
                        node = new DialogueNode(dialogueSection);
                        _graphView.AddElement(node);
                        nodeDictionary.Add(section.guid, node);
                        break;
                    default:
                        Debug.Log("Unknown BaseNvData");
                        break;
                }
            }

            foreach (var edge in from section in dataDictionary.Values
                     where !string.IsNullOrEmpty(section.nextGuid)
                     select new Edge
                     {
                         output = nodeDictionary[section.guid].outputContainer[0].Q<Port>(),
                         input = nodeDictionary[section.nextGuid].inputContainer[0].Q<Port>()
                     })
            {
                edge.input.Connect(edge);
                edge.output.Connect(edge);
                _graphView.AddElement(edge);
            }
        }

        private void PlotSave()
        {
            var graphNodes = _graphView.graphElements;

            var dialogueNodeList = graphNodes.Where(a => a is DialogueNode).Cast<DialogueNode>().ToList();
            var startNodeList = graphNodes.Where(a => a is StartNode).Cast<StartNode>().ToList();

            var collection = new List<BaseNvData>();

            foreach (var dialogueNode in dialogueNodeList)
            {
                NodeDataSave<DialogueNvData>(dialogueNode, ref collection);
            }

            foreach (var startNode in startNodeList)
            {
                NodeDataSave<StartNvData>(startNode, ref collection);
            }

            var dataDictionary = collection.ToDictionary(sec => sec.guid);
            _plotSo.nodesData = dataDictionary;
            EditorUtility.SetDirty(_plotSo);
        }


        private static void NodeDataSave<T>(Node targetNode, ref List<BaseNvData> collection)
            where T : BaseNvData
        {
            var data = targetNode.userData as T;
            if (data == null) throw new Exception("Data type mismatch");
            data.nodePos = targetNode.GetPosition();
            var nodeEdges = targetNode.outputContainer.Q<Port>().connections.ToList();
            if (nodeEdges.Count > 0)
            {
                data.nextGuid = nodeEdges.First().input.node.viewDataKey;
                //Todo:Choice node has many ports
            }

            collection.Add(data);
        }

        private void SearchTreeBuild(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            //create a search windows under the cursor
            var worldMousePosition = _window.position.position + Event.current.mousePosition;
            var searchWindowContext = new SearchWindowContext(worldMousePosition);
            var searchWindowProvider = CreateInstance<NodeSearchWindowProvider>();
            searchWindowProvider.Init(_graphView, _window);
            SearchWindow.Open(searchWindowContext, searchWindowProvider);
        }

        private void ToolBarMenuAction()
        {
            _toolbarMenu.menu.AppendAction("Test", _ => { Debug.Log("Test Successful"); });
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            PlotSave();
        }

        /*[OnOpenAsset(2)]
        public static bool OnOpenAssets(int id, int line)
        {
            if (_window)
            {
                _window.Show();
                return true;
            }

            _window = GetWindow<PlotSoEditorWindow>();
            if (EditorUtility.InstanceIDToObject(id) is not PlotSo tree) return false;
            _window._plotSo = tree;
            _window.WindowInitialize();
            _window.Show();
            return true;
        }*/

        private void OnInspectorUpdate()
        {
            _graphView.OnUpdate?.Invoke();
            _graphView.GetBlackboard().visible = _inspectorToggle.value;
        }

        private void OnDestroy()
        {
            if (_autoSaveToggle.value) PlotSave();
        }
    }
}