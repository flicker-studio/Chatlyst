using System.Collections.Generic;
using Chatlyst.Editor;
using Chatlyst.Runtime;
using Chatlyst.Runtime.Data;
using NUnit.Framework;
using TheEditorWindow = UnityEditor.EditorWindow;


namespace Tests.Editor
{
    public class EditorWindow
    {
        [Test]
        public void UserDataTypeConversion()
        {
            var begins =
                new List<BasicNode>
                {
                    new Chatlyst.Runtime.BeginNode("Start Label", 2),
                    new Chatlyst.Runtime.BeginNode("Start Label", 3)
                };

            var index = new Chatlyst.Runtime.NodeIndex();
            index.AutoAddNodes(begins);

            ChatlystEditorWindow.EditorWindow = (ChatlystEditorWindow)TheEditorWindow.GetWindow(typeof(ChatlystEditorWindow));
            var view = new ChatlystGraphView();
            view.GraphInitialize(ChatlystEditorWindow.EditorWindow);

            view.BuildFromNodeIndex(index);
            var newIndex = view.GetNodeIndex();
            ChatlystEditorWindow.EditorWindow.Close();
            Assert.AreEqual(index, newIndex);
        }
    }
}
