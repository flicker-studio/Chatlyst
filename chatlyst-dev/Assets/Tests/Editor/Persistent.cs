using System;
using System.Collections.Generic;
using Chatlyst.Editor;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class NodeIndexTest
{
    [Test]
    public void AutoAddNodeFunction()
    {
        var begins =
            new List<BasicNode>
            {
                new BeginNode("Start Label", 2),
                new BeginNode("Start Label", 3)
            };
        var index = new NodeIndex();
        index.AutoAddNodes(begins);
        string json = index.ToJson();
        Debug.Log(json);
        var deserialize = IndexJsonInternal.Deserialize(json);
        Assert.AreEqual(index, deserialize);
    }
}
public class WindowsTest
{
    [Test]
    public void UserDataTypeConversion()
    {
        var begins =
            new List<BasicNode>
            {
                new BeginNode("Start Label", 2),
                new BeginNode("Start Label", 3)
            };
        var index = new NodeIndex();
        index.AutoAddNodes(begins);

        ChatlystEditorWindow.EditorWindow = (ChatlystEditorWindow)EditorWindow.GetWindow(typeof(ChatlystEditorWindow));
        var view = new ChatlystGraphView();
        view.GraphInitialize(ChatlystEditorWindow.EditorWindow);

        view.BuildFromNodeIndex(index);
        var newIndex = view.GetNodeIndex();
        ChatlystEditorWindow.EditorWindow.Close();
        Assert.AreEqual(index, newIndex);
    }
}
