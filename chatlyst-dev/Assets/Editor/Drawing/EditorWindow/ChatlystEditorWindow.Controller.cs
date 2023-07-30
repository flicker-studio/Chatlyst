using UnityEditor;

namespace Chatlyst.Editor
{
    partial class ChatlystEditorWindow : EditorWindow
    {
        public void Initialize(in string id)
        {
            DataLoader(id);
            ViewLoader();

            _saveButton.clicked += SaveChanges;
            GraphView.GraphInitialize(this);
            WindowConfig();
            RebuildFromDisk();
        }


        public void Update()
        {
            GraphView.graphViewChanged += _ =>
            {
                hasUnsavedChanges = true;
                return default;
            };

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
