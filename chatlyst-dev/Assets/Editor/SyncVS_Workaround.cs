using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.CodeEditor;
using System.Reflection;
using Microsoft.Unity.VisualStudio.Editor;
 
// The recommended way to create the VisualStudio SLN from the command line is a call
// Unity.exe -executeMethod "UnityEditor.SyncVS.SyncSolution"
//
// Unfortunately, as of Unithy 2021.3.21f1 the built-in UnityEditor.SyncVS.SyncSolution internally calls
// Unity.CodeEditor.CodeEditor.Editor.CurrentCodeEditor.SyncAll() where CurrentCodeEditor depends on the user preferences
// which may not actually be set to VS on a CI machine.
// (see https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/CodeEditor/SyncVS.cs)
//
// This routine provides an re-implementation that avoids reliability on the preference setting
// Unity.exe -executeMethod "UnityEditor.SyncVS.SyncSolution"
public static class SyncVS_Workaround
{
    public static void SyncSolution()
    {
        // Ensure that the mono islands are up-to-date
        AssetDatabase.Refresh();
 
        List<IExternalCodeEditor> externalCodeEditors;
 
        // externalCodeEditors = Unity.CodeEditor.Editor.m_ExternalCodeEditors;
        // ... unfortunately this is private without any means of access. Use reflection to get the value ...
        externalCodeEditors = CodeEditor.Editor.GetType().GetField("m_ExternalCodeEditors", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(CodeEditor.Editor) as List<IExternalCodeEditor>;
 
        foreach (var externalEditor in externalCodeEditors)
            if (externalEditor is VisualStudioEditor vse)
            {
                vse.SyncAll();
                Debug.Log($"called {vse}.SyncAll");
                return;
            }
 
        // When com.unity.ide.visualstudio is installed, we should never get here
        Debug.LogError("no VisualStudioEditor registered");
    }
}
