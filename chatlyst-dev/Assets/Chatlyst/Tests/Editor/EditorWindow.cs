using System.Collections.Generic;
using Chatlyst.Editor;
using Chatlyst.Runtime.Data;
using Chatlyst.Runtime.Serialization;
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

            var index = new NodeDataIndex();
            index.AutoAddNodes(begins);

            ChatlystEditorWindow.EditorWindow = (ChatlystEditorWindow)TheEditorWindow.GetWindow(typeof(ChatlystEditorWindow));
            var view = new ChatlystGraphView();
            view.Initialize(index.ToString());

            var newIndex = view.nodeDataIndex;
            ChatlystEditorWindow.EditorWindow.Close();
            Assert.AreEqual(index, newIndex);
        }
    }
}
