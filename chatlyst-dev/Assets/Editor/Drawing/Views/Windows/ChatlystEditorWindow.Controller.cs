using System;
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
            DataToView();
        }


        private bool DataToView()
        {
            var nodeDataIndex = NodeIndex.DeserializeFromJson(_jsonData);
            if (nodeDataIndex == null) throw new Exception("Deserialize failed!");
            return GraphView.BuildFromNodeIndex(nodeDataIndex);
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
