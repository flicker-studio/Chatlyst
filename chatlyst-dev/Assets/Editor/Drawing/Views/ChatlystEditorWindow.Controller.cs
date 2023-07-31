using System;
using System.Linq;
using UnityEditor;
using static Chatlyst.Editor.Serialization.NexusJsonInternal;

namespace Chatlyst.Editor
{
    partial class ChatlystEditorWindow : EditorWindow
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
            var entityIEnumerable = Deserialize(_jsonData);
            if (entityIEnumerable == null) throw new Exception("Deserialize failed!");
            var entityList = entityIEnumerable.ToList();
            return GraphView.BuildFromEntries(entityList);
        }

        public void OnDestroy() => _onDestroy?.Invoke();

        public void Update()
        {
            _onUpdate?.Invoke();

            if (GraphView == null)
            {
                if (_assetGuid != null)
                {
                    Initialize(_assetGuid);
                }
                else
                {
                    Close();
                }
            }
            else
            {
                GraphView.InspectorNode();
            }
        }
    }
}
