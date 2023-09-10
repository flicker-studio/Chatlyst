using System;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime;
using UnityEditor;

namespace Chatlyst.Editor
{
    public partial class ChatlystEditorWindow : EditorWindow
    {
        private Action _onUpdate;
        private Action _onDestroy;

        public void Initialize(in string id)
        {
            DataLoader(id);
            ViewLoader();
        }

        public void Update()
        {
            _onUpdate?.Invoke();
        }

        public void OnDestroy()
        {
            _onDestroy?.Invoke();
        }
    }
}
