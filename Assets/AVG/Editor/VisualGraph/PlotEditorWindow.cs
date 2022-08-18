using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.VisualGraph
{
    public class PlotEditorWindow : EditorWindow
    {
        private PlotGraphView m_GraphView;

        public static void Edit()
        {
            var window = GetWindow<PlotEditorWindow>("Story");
            window.CreateGraphView();
        }

        private void CreateGraphView()
        {
            m_GraphView = new PlotGraphView();
            m_GraphView.RegisterCallback<KeyDownEvent>(SpaceKeyMenu);
            m_GraphView.StretchToParentSize();
            rootVisualElement.Add(m_GraphView);
        }

        private void SpaceKeyMenu(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != KeyCode.Space) return;
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Add Node"), true, () => { m_GraphView.CreatNode(); });
            menu.ShowAsContext();
        }
    }
}