using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chatlyst.Editor
{
    public class CustomSettingProvider : SettingsProvider
    {
        private const string Path = "Assets/Settings/CustomSetting.asset";
        private static SerializedObject _settings;

        private CustomSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(
            path, scopes, keywords)
        {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = new SerializedObject(GetSettings());
        }

        private class Styles
        {
            public static readonly GUIContent Start = new GUIContent("Node");
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.ObjectField(_settings.FindProperty("nodeSetting"), Styles.Start);
            _settings.ApplyModifiedPropertiesWithoutUndo();
        }

        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            var provider = new CustomSettingProvider("Project/Custom Settings", SettingsScope.Project);
            if (_settings != null) provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;
        }

        public static CustomSetting GetSettings()
        {
            return AssetDatabase.LoadAssetAtPath<CustomSetting>(Path);
        }
    }
}