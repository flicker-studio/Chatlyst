using System.Collections.Generic;
using NexusVisual.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    public class NodeSettingProvider : SettingsProvider
    {
        private static SerializedObject _settings;

        private NodeSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(
            path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = new SerializedObject(CustomSettingProvider.GetSettings().nodeSetting);
        }

        private class Styles
        {
            public static readonly GUIContent Start = new GUIContent("Start Node");
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(_settings.FindProperty("startNode"), Styles.Start);
            _settings.ApplyModifiedPropertiesWithoutUndo();
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new NodeSettingProvider("Project/Custom Settings/Node", SettingsScope.Project);
            if (_settings != null) provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;
        }
    }
}