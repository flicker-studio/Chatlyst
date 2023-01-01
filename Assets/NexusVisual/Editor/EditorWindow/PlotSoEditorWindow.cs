using System;
using System.Collections.Generic;
using System.Linq;
using NexusVisual.Runtime;
using UnityEditor;
using UnityEditor.Callbacks;
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
        private PlotSoGraphView _soGraphView;
        private static PlotSoEditorWindow _window;

        //Toolbar element
        private ToolbarMenu _toolbarMenu;
        private ToolbarToggle _inspectorToggle;
        private ToolbarToggle _autoSaveToggle;
        private ToolbarButton _save;


        private void WindowInitialize()
        {
            //Initialize editor window
            var visualTree = EditorGUIUtility.Load("NodeEditorWindow.uxml") as VisualTreeAsset;
            if (!visualTree) throw new Exception("Can not find EditorWindow.uxml");
            visualTree.CloneTree(rootVisualElement);
            saveChangesMessage = "未保存的更改!\n您是否要保存？";
            titleContent.text = $"{_plotSo.name}";

            //Get element
            _soGraphView = rootVisualElement.Q<PlotSoGraphView>("GraphView");
            _toolbarMenu = rootVisualElement.Q<ToolbarMenu>("Menu");
            _inspectorToggle = rootVisualElement.Q<ToolbarToggle>("Inspector");
            _autoSaveToggle = rootVisualElement.Q<ToolbarToggle>("AutoSave");
            _save = rootVisualElement.Q<ToolbarButton>("Save");


            //Action bind
            _soGraphView.RegisterCallback<KeyDownEvent>(KeyDownMenuTrigger);
            _soGraphView.graphViewChanged += (_ =>
            {
                hasUnsavedChanges = true;
                return default;
            });
            _save.clicked += SaveChanges;
            ToolBarMenuAction();

            #region Re-draw the plot tree

            //Todo:Use cache
            var sectionDictionary = _plotSo.BaseSectionDic;
            var nodeDictionary = new Dictionary<string, Node>();

            foreach (var section in sectionDictionary.Values)
            {
                Node node;
                switch (section)
                {
                    case StartSection startSection:
                        node = new StartNode(startSection);
                        _soGraphView.AddElement(node);
                        nodeDictionary.Add(section.Guid, node);
                        break;
                    case DialogueSection dialogueSection:
                        node = new DialogueNode(dialogueSection);
                        _soGraphView.AddElement(node);
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

        private void DataSave()
        {
            var collection = CreateInstance<PlotSo>();
            var edgeList = _soGraphView.edges.ToList();
            var nodeList = _soGraphView.graphElements;
            var dialogueNodeList = nodeList.Where(a => a is DialogueNode).Cast<DialogueNode>().ToList();
            var startNodeList = nodeList.Where(a => a is StartNode).Cast<StartNode>().ToList();


            foreach (var dialogueNode in dialogueNodeList)
            {
                var data = dialogueNode.userData as DialogueSection;
                if (data == null) return;
                data.Pos = dialogueNode.GetPosition();
                //  dialogueNode.edge
                collection.dialogueSections.Add(data);
            }


            foreach (var startNode in startNodeList)
            {
                var data = startNode.userData as StartSection;
                if (data == null) return;
                data.Pos = startNode.GetPosition();
                //  dialogueNode.edge
                collection.startSections.Add(data);
            }


            #region Node

/*
            foreach (var sectionNode in nodeList)
            {
                var next = sectionNode.outputContainer.Q<Port>().connections.First().input;


                /*   switch (sectionNode)
                   {
                       case DialogueNode dialogueNode:
                           dialogueNode.userData.Pos = dialogueNode.GetPosition();
                           //  dialogueNode.edge
                           collection.dialogueSections.Add(dialogueNode.userData as DialogueSection);
                           break;
                       case StartNode startNode:
                           startNode.data.Pos = startNode.GetPosition();
                           collection.startSections.Add(startNode.data);
                           break;
                       default:
                           Debug.Log("Unknown Node");
                           break;
                   }
        }
        */

            #endregion

            #region Link

            var sectionDictionary = collection.BaseSectionDic;
            foreach (var edge in edgeList)
            {
                var thisGuid = edge.output.node.viewDataKey;
                var nextGuid = edge.input.node.viewDataKey;
                sectionDictionary[thisGuid].Next = nextGuid;
            }

            _plotSo.SectionCollect(sectionDictionary);
            EditorUtility.SetDirty(_plotSo);

            #endregion
        }

        private void KeyDownMenuTrigger(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            //create a search windows under the cursor
            var worldMousePosition = _window.position.position + Event.current.mousePosition;
            var searchWindowContext = new SearchWindowContext(worldMousePosition);
            var searchWindowProvider = CreateInstance<NodeSearchWindowProvider>();
            searchWindowProvider.Init(_soGraphView, _window);
            SearchWindow.Open(searchWindowContext, searchWindowProvider);
        }


        private void ToolBarMenuAction()
        {
            _toolbarMenu.menu.AppendAction("Test", _ => { Debug.Log("Test Successful"); });
        }

        #region Event function

        [OnOpenAsset(1)]
        public static bool OnOpenAssets(int id, int line)
        {
            if (!_window)
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
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            DataSave();
        }

        private void OnInspectorUpdate()
        {
            _soGraphView.Inspector.visible = _inspectorToggle.value;
            _soGraphView.Inspector.Inspector();
        }

        private void OnDestroy()
        {
            if (_autoSaveToggle.value) DataSave();
        }

        #endregion
    }
}